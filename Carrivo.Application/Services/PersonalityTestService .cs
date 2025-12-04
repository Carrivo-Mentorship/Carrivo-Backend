using Carrivo.Application.DTOs.Common;
using Carrivo.Application.DTOs.Personality_Test;
using Carrivo.Application.Interfaces;
using Carrivo.Core.Entities;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Carrivo.Application.Services.Test;

public class PersonalityTestService : IPersonalityTestService
{
    private readonly CarrivoDbContext _context;

    public PersonalityTestService(CarrivoDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<bool>> SaveTestProgressAsync(SaveTestProgressRequest request)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == request.UserId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<bool>.Failure("Student profile not found", statusCode: 404);

        // الحصول على أحدث test attempt غير مكتمل
        var testAttempt = await _context.StudentTestAttempts
            .Where(sta => sta.StudentProfileId == studentProfile.Id && !sta.IsCompleted)
            .OrderByDescending(sta => sta.StartedAt)
            .FirstOrDefaultAsync();

        // إذا لم يوجد، إنشاء واحد جديد
        if (testAttempt == null)
        {
            var latestTest = await _context.PersonalityTests
                .OrderByDescending(pt => pt.CreatedAt)
                .FirstOrDefaultAsync();

            if (latestTest == null)
                return ApiResponse<bool>.Failure("No personality test available", statusCode: 404);

            testAttempt = new StudentTestAttempt
            {
                Id = Guid.NewGuid(),
                StudentProfileId = studentProfile.Id,
                PersonalityTestId = latestTest.Id,
                StartedAt = DateTime.UtcNow,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.StudentTestAttempts.Add(testAttempt);
        }

        // حفظ Progress كـ JSON في MlModelOutput مؤقتاً
        var progressData = new
        {
            Answers = request.Answers,
            CurrentPage = request.CurrentPage
        };

        testAttempt.MlModelOutput = JsonSerializer.Serialize(progressData);
        testAttempt.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return ApiResponse<bool>.Success(true, "Progress saved successfully");
    }

    public async Task<ApiResponse<TestResultDto>> SubmitTestAsync(SubmitTestRequest request)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == request.UserId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<TestResultDto>.Failure("Student profile not found", statusCode: 404);
        var testAttempt = await _context.StudentTestAttempts
            .Where(sta => sta.StudentProfileId == studentProfile.Id && !sta.IsCompleted)
            .OrderByDescending(sta => sta.StartedAt)
            .FirstOrDefaultAsync();

        if (testAttempt == null)
            return ApiResponse<TestResultDto>.Failure("No active test attempt found", statusCode: 404);

        // TODO: هنا يتم استدعاء ML Model لتحليل الإجابات
        // مؤقتاً سنستخدم logic بسيط

        // حساب النتيجة (مثال)
        var scores = new Dictionary<string, decimal>
        {
            { "Analytical", 0 },
            { "Creative", 0 },
            { "Social", 0 },
            { "Practical", 0 }
        };

        // Get top 3 personality types based on scores
        var topPersonalityType = await _context.PersonalityTypes
            .OrderBy(pt => Guid.NewGuid()) // مؤقتاً random
            .FirstOrDefaultAsync();

        if (topPersonalityType == null)
            return ApiResponse<TestResultDto>.Failure("No personality types configured", statusCode: 500);

        // Get track recommendations
        var recommendations = await _context.PersonalityTypeTracks
            .Where(ptt => ptt.PersonalityTypeId == topPersonalityType.Id)
            .Include(ptt => ptt.Track)
            .OrderByDescending(ptt => ptt.CompatibilityScore)
            .Take(3)
            .Select((ptt, index) => new RecommendationDto
            {
                TrackId = ptt.TrackId,
                TrackName = ptt.Track.Code,
                CompatibilityScore = ptt.CompatibilityScore,
                Rank = index + 1
            })
            .ToListAsync();

        // Mark test as completed
        testAttempt.IsCompleted = true;
        testAttempt.CompletedAt = DateTime.UtcNow;
        testAttempt.PersonalityTypeId = topPersonalityType.Id;
        testAttempt.MlModelOutput = JsonSerializer.Serialize(new
        {
            Scores = scores,
            PersonalityType = topPersonalityType.Code
        });

        // Save recommendations
        foreach (var rec in recommendations)
        {
            _context.TestRecommendations.Add(new TestRecommendation
            {
                Id = Guid.NewGuid(),
                StudentTestAttemptId = testAttempt.Id,
                TrackId = rec.TrackId,
                Score = rec.CompatibilityScore,
                Rank = rec.Rank,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();

        var result = new TestResultDto
        {
            CareerPath = topPersonalityType.Code,
            Score = scores,
            Recommendations = recommendations
        };

        return ApiResponse<TestResultDto>.Success(result, "Test submitted successfully");
    }

    public async Task<ApiResponse<TestProgressDto>> GetTestProgressAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<TestProgressDto>.Failure("Student profile not found", statusCode: 404);

        var testAttempt = await _context.StudentTestAttempts
            .Where(sta => sta.StudentProfileId == studentProfile.Id && !sta.IsCompleted)
            .OrderByDescending(sta => sta.StartedAt)
            .FirstOrDefaultAsync();

        if (testAttempt == null || string.IsNullOrEmpty(testAttempt.MlModelOutput))
        {
            return ApiResponse<TestProgressDto>.Success(new TestProgressDto
            {
                Answers = new Dictionary<string, int>(),
                CurrentPage = 1
            });
        }

        var progressData = JsonSerializer.Deserialize<TestProgressDto>(testAttempt.MlModelOutput);

        return ApiResponse<TestProgressDto>.Success(progressData!, "Progress retrieved successfully");
    }
}