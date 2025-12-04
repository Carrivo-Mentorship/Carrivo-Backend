using Carrivo.Application.DTOs.Common;

using Carrivo.Application.DTOs.User_Profile;
using Carrivo.Application.Interfaces;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Carrivo.Application.Services.User;

public class UserService : IUserService
{
    private readonly CarrivoDbContext _context;

    public UserService(CarrivoDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<UserProfileDto>> GetUserProfileAsync(Guid userId)
    {
        var user = await _context.Users
            .Where(u => u.Id == userId && !u.IsDeleted)
            .Select(u => new UserProfileDto
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                Email = u.Email!,
                Image = u.ProfilePictureUrl
            })
            .FirstOrDefaultAsync();

        if (user == null)
            return ApiResponse<UserProfileDto>.Failure("User not found", statusCode: 404);

        return ApiResponse<UserProfileDto>.Success(user);
    }

    public async Task<ApiResponse<CurrentPathDto>> GetCurrentPathAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<CurrentPathDto>.Failure("Student profile not found", statusCode: 404);

        var activeRoadmap = await _context.StudentRoadmaps
            .Where(sr => sr.StudentProfileId == studentProfile.Id && sr.IsActive && !sr.IsDeleted)
            .Include(sr => sr.Roadmap)
                .ThenInclude(r => r.Track)
            .OrderByDescending(sr => sr.CreatedAt)
            .FirstOrDefaultAsync();

        if (activeRoadmap == null)
            return ApiResponse<CurrentPathDto>.Failure("No active path found", statusCode: 404);

        var result = new CurrentPathDto
        {
            PathId = activeRoadmap.RoadmapId,
            Title = activeRoadmap.Roadmap.Track.Code,
            Description = activeRoadmap.Roadmap.Description,
            Image = null, // يمكن إضافة حقل Image للـ Track
            Progress = activeRoadmap.ProgressPercentage
        };

        return ApiResponse<CurrentPathDto>.Success(result);
    }

    public async Task<ApiResponse<PathOverviewDto>> GetPathOverviewAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<PathOverviewDto>.Failure("Student profile not found", statusCode: 404);

        var activeRoadmap = await _context.StudentRoadmaps
            .Where(sr => sr.StudentProfileId == studentProfile.Id && sr.IsActive && !sr.IsDeleted)
            .FirstOrDefaultAsync();

        if (activeRoadmap == null)
            return ApiResponse<PathOverviewDto>.Failure("No active path found", statusCode: 404);

        var milestones = await _context.RoadmapMilestones
            .Where(rm => rm.RoadmapId == activeRoadmap.RoadmapId && !rm.IsDeleted)
            .OrderBy(rm => rm.OrderIndex)
            .Select(rm => new
            {
                rm.Id,
                rm.Title,
                Tasks = rm.Tasks.Where(t => !t.IsDeleted).Select(t => t.Id).ToList()
            })
            .ToListAsync();

        var overview = new List<MilestoneProgressDto>();

        foreach (var milestone in milestones)
        {
            if (milestone.Tasks.Count == 0)
            {
                overview.Add(new MilestoneProgressDto
                {
                    Title = milestone.Title,
                    Progress = 0
                });
                continue;
            }

            var completedTasks = await _context.StudentProgress
                .Where(sp => sp.StudentProfileId == studentProfile.Id
                    && milestone.Tasks.Contains(sp.MilestoneTaskId)
                    && sp.IsCompleted)
                .CountAsync();

            var progress = (decimal)completedTasks / milestone.Tasks.Count * 100;

            overview.Add(new MilestoneProgressDto
            {
                Title = milestone.Title,
                Progress = Math.Round(progress, 0)
            });
        }

        return ApiResponse<PathOverviewDto>.Success(new PathOverviewDto { Overview = overview });
    }

    public async Task<ApiResponse<UserMentorDto>> GetUserMentorAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<UserMentorDto>.Failure("Student profile not found", statusCode: 404);

        var activeSession = await _context.MentorshipSessions
            .Where(ms => ms.StudentProfileId == studentProfile.Id
                && (ms.Status == Core.Enums.MentorshipSessionStatus.Active
                    || ms.Status == Core.Enums.MentorshipSessionStatus.PendingPayment)
                && !ms.IsDeleted)
            .Include(ms => ms.MentorProfile)
                .ThenInclude(mp => mp.User)
            .Include(ms => ms.MentorProfile)
                .ThenInclude(mp => mp.MentorTracks)
                    .ThenInclude(mt => mt.Track)
            .OrderByDescending(ms => ms.CreatedAt)
            .FirstOrDefaultAsync();

        if (activeSession == null)
        {
            return ApiResponse<UserMentorDto>.Success(new UserMentorDto
            {
                HasMentor = false,
                Mentor = null
            });
        }

        var mentor = activeSession.MentorProfile;
        var speciality = mentor.MentorTracks.FirstOrDefault()?.Track.Code ?? mentor.JobTitle;

        return ApiResponse<UserMentorDto>.Success(new UserMentorDto
        {
            HasMentor = true,
            Mentor = new MentorBasicDto
            {
                Id = mentor.Id,
                Name = $"{mentor.User.FirstName} {mentor.User.LastName}",
                Speciality = speciality,
                Image = mentor.User.ProfilePictureUrl
            }
        });
    }

    public async Task<ApiResponse<QuickAccessDto>> GetQuickAccessAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<QuickAccessDto>.Failure("Student profile not found", statusCode: 404);

        var hasRoadmap = await _context.StudentRoadmaps
            .AnyAsync(sr => sr.StudentProfileId == studentProfile.Id && sr.IsActive && !sr.IsDeleted);

        // TODO: إضافة جدول للـ Saved Resources
        var savedResources = 0;

        // TODO: إضافة جدول للـ Achievements
        var achievements = 0;

        return ApiResponse<QuickAccessDto>.Success(new QuickAccessDto
        {
            Roadmap = hasRoadmap,
            SavedResources = savedResources,
            Achievements = achievements
        });
    }

    public async Task<ApiResponse<NotificationCountDto>> GetNotificationCountAsync(Guid userId)
    {
        var unreadCount = await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead && !n.IsDeleted)
            .CountAsync();

        return ApiResponse<NotificationCountDto>.Success(new NotificationCountDto
        {
            Unread = unreadCount
        });
    }
}