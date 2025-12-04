using Carrivo.Application.DTOs.Common;
using Carrivo.Application.DTOs.Mentor_System;
using Carrivo.Application.Interfaces;
using Carrivo.Core.Entities;
using Carrivo.Core.Enums;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Carrivo.Application.Services.Mentor;

public class MentorService : IMentorService
{
    private readonly CarrivoDbContext _context;

    public MentorService(CarrivoDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<MentorListDto>> GetMentorsAsync(
        string? search = null,
        string? sort = null,
        string? experience = null,
        string? availability = null,
        decimal? rating = null,
        decimal? price = null)
    {
        var query = _context.MentorProfiles
            .Where(mp => !mp.IsDeleted)
            .Include(mp => mp.User)
            .Include(mp => mp.MentorTracks)
                .ThenInclude(mt => mt.Track)
            .AsQueryable();

        // Search Filter
        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(mp =>
                mp.User.FirstName.ToLower().Contains(searchLower) ||
                mp.User.LastName.ToLower().Contains(searchLower) ||
                mp.JobTitle.ToLower().Contains(searchLower));
        }

        // Experience Filter
        if (!string.IsNullOrWhiteSpace(experience))
        {
            query = experience.ToLower() switch
            {
                "junior" => query.Where(mp => mp.YearsOfExperience <= 2),
                "mid" => query.Where(mp => mp.YearsOfExperience > 2 && mp.YearsOfExperience <= 5),
                "senior" => query.Where(mp => mp.YearsOfExperience > 5),
                _ => query
            };
        }

        // Rating Filter
        if (rating.HasValue)
        {
            query = query.Where(mp => mp.AverageRating >= rating.Value);
        }

        // Price Filter
        if (price.HasValue)
        {
            query = query.Where(mp => mp.PricePerHour <= price.Value);
        }

        // Sorting
        query = sort?.ToLower() switch
        {
            "rating" => query.OrderByDescending(mp => mp.AverageRating),
            "price_asc" => query.OrderBy(mp => mp.PricePerHour),
            "price_desc" => query.OrderByDescending(mp => mp.PricePerHour),
            "experience" => query.OrderByDescending(mp => mp.YearsOfExperience),
            _ => query.OrderByDescending(mp => mp.AverageRating) // default
        };

        var mentors = await query
            .Select(mp => new MentorCardDto
            {
                Id = mp.Id,
                Name = $"{mp.User.FirstName} {mp.User.LastName}",
                JobTitle = mp.JobTitle,
                Rating = mp.AverageRating,
                Price = mp.PricePerHour,
                Image = mp.User.ProfilePictureUrl,
                ExperienceLevel = mp.YearsOfExperience <= 2 ? "junior" :
                                 mp.YearsOfExperience <= 5 ? "mid" : "senior",
                Availability = "online", // TODO: إضافة logic للـ availability
                Track = mp.MentorTracks.FirstOrDefault() != null ?
                        mp.MentorTracks.First().Track.Code : ""
            })
            .ToListAsync();

        return ApiResponse<MentorListDto>.Success(new MentorListDto { Mentors = mentors });
    }

    public async Task<ApiResponse<MentorDetailDto>> GetMentorByIdAsync(Guid mentorId)
    {
        var mentor = await _context.MentorProfiles
            .Where(mp => mp.Id == mentorId && !mp.IsDeleted)
            .Include(mp => mp.User)
            .Include(mp => mp.MentorTracks)
                .ThenInclude(mt => mt.Track)
            .Include(mp => mp.MentorshipSessions)
                .ThenInclude(ms => ms.StudentProfile)
                    .ThenInclude(sp => sp.User)
            .FirstOrDefaultAsync();

        if (mentor == null)
            return ApiResponse<MentorDetailDto>.Failure("Mentor not found", statusCode: 404);

        // Get reviews from completed sessions
        var reviews = mentor.MentorshipSessions
            .Where(ms => ms.Status == MentorshipSessionStatus.Completed && ms.Rating.HasValue)
            .Select(ms => new ReviewDto
            {
                StudentId = ms.StudentProfile.UserId,
                StudentName = $"{ms.StudentProfile.User.FirstName} {ms.StudentProfile.User.LastName}",
                Rating = ms.Rating!.Value,
                Comment = ms.Review,
                ReviewedAt = ms.ReviewedAt ?? ms.UpdatedAt ?? ms.CreatedAt
            })
            .OrderByDescending(r => r.ReviewedAt)
            .ToList();

        // Extract skills from tracks
        var skills = mentor.MentorTracks.Select(mt => mt.Track.Code).ToList();

        var result = new MentorDetailDto
        {
            Id = mentor.Id,
            Name = $"{mentor.User.FirstName} {mentor.User.LastName}",
            Bio = mentor.ProfessionalBio,
            Reviews = reviews,
            Skills = skills,
            ExperienceSummary = $"{mentor.YearsOfExperience} years of experience in {mentor.JobTitle}",
            Price = mentor.PricePerHour,
            Rating = mentor.AverageRating,
            Image = mentor.User.ProfilePictureUrl
        };

        return ApiResponse<MentorDetailDto>.Success(result);
    }

    public async Task<ApiResponse<bool>> RequestSessionAsync(RequestSessionRequest request)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == request.UserId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<bool>.Failure("Student profile not found", statusCode: 404);

        var mentorProfile = await _context.MentorProfiles
            .FirstOrDefaultAsync(mp => mp.Id == request.MentorId && !mp.IsDeleted);

        if (mentorProfile == null)
            return ApiResponse<bool>.Failure("Mentor not found", statusCode: 404);

        // Get student's active track
        var activeRoadmap = await _context.StudentRoadmaps
            .Where(sr => sr.StudentProfileId == studentProfile.Id && sr.IsActive)
            .Include(sr => sr.Roadmap)
            .FirstOrDefaultAsync();

        if (activeRoadmap == null)
            return ApiResponse<bool>.Failure("No active learning path found", statusCode: 400);

        // Check if there's already a pending/active request with this mentor
        var existingRequest = await _context.MentorshipRequests
            .AnyAsync(mr => mr.StudentProfileId == studentProfile.Id
                && mr.MentorProfileId == request.MentorId
                && (mr.Status == MentorshipRequestStatus.Pending
                    || mr.Status == MentorshipRequestStatus.Accepted));

        if (existingRequest)
            return ApiResponse<bool>.Failure("You already have a pending or active request with this mentor", statusCode: 400);

        // Create mentorship request
        var mentorshipRequest = new MentorshipRequest
        {
            Id = Guid.NewGuid(),
            StudentProfileId = studentProfile.Id,
            MentorProfileId = request.MentorId,
            TrackId = activeRoadmap.Roadmap.TrackId,
            Message = request.Message,
            Status = MentorshipRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _context.MentorshipRequests.Add(mentorshipRequest);
        await _context.SaveChangesAsync();

        // TODO: Send notification to mentor

        return ApiResponse<bool>.Success(true, "Session request sent successfully");
    }
}