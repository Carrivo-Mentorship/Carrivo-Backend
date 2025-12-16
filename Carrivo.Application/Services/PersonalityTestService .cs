using Carrivo.Application.DTOs.Common;
using Carrivo.Application.DTOs.Personality_Test;
using Carrivo.Application.Interfaces;
using Carrivo.Core.Entities;
using Carrivo.Core.Enums;
using Carrivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
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

    public async Task<ApiResponse<bool>> SaveTestResultAsync(SaveTestResultRequest request)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == request.UserId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<bool>.Failure("Student profile not found", statusCode: 404);

        studentProfile.TestCareerCategory = request.CareerCategory;
        studentProfile.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return ApiResponse<bool>.Success(true, "Test result saved successfully");
    }

    public async Task<ApiResponse<TestResultResponse>> GetTestResultAsync(Guid userId)
    {
        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(sp => sp.UserId == userId && !sp.IsDeleted);

        if (studentProfile == null)
            return ApiResponse<TestResultResponse>.Failure("Student profile not found", statusCode: 404);

        if (studentProfile.TestCareerCategory == null)
            return ApiResponse<TestResultResponse>.Failure("No test result found for this user", statusCode: 404);

        var category = studentProfile.TestCareerCategory.Value;
        var categoryName = category.ToString();
        
        // Get all career results for this category
        var careerResults = GetCareerResultsByCategory(category);

        var response = new TestResultResponse
        {
            CategoryName = categoryName,
            CareerResults = careerResults
        };

        return ApiResponse<TestResultResponse>.Success(response, "Test result retrieved successfully");
    }

    private static List<CareerResultItem> GetCareerResultsByCategory(CareerCategory category)
    {
        var results = new List<CareerResultItem>();

        var mapping = new Dictionary<CareerCategory, CareerResult[]>
        {
            { CareerCategory.AIML, new[] { CareerResult.AIEngineer, CareerResult.AIAndDataScientist, CareerResult.MachineLearning, CareerResult.MLOps } },
            { CareerCategory.Data, new[] { CareerResult.DataAnalyst, CareerResult.BIAnalyst, CareerResult.DataEngineer } },
            { CareerCategory.Software, new[] { CareerResult.Frontend, CareerResult.Backend, CareerResult.FullStack, CareerResult.SoftwareArchitect, CareerResult.DeveloperRelations, CareerResult.ProductManager, CareerResult.TechnicalWriter, CareerResult.DevOps, CareerResult.QA, CareerResult.EngineeringManager } },
            { CareerCategory.Security, new[] { CareerResult.CyberSecurity } },
            { CareerCategory.Game, new[] { CareerResult.GameDeveloper, CareerResult.ServerSideGameDeveloper } },
            { CareerCategory.Mobile, new[] { CareerResult.Android, CareerResult.iOS } },
            { CareerCategory.UXDesign, new[] { CareerResult.UXDesign } },
            { CareerCategory.Blockchain, new[] { CareerResult.Blockchain } }
        };

        if (mapping.TryGetValue(category, out var careers))
        {
            foreach (var career in careers)
            {
                results.Add(new CareerResultItem
                {
                    Id = (int)career,
                    Name = FormatCareerName(career.ToString())
                });
            }
        }

        return results;
    }

    private static string FormatCareerName(string enumName)
    {
        // Convert PascalCase to readable format (e.g., "AIEngineer" -> "AI Engineer")
        var result = new StringBuilder();
        foreach (var c in enumName)
        {
            if (char.IsUpper(c) && result.Length > 0)
            {
                // Check if previous char was also uppercase (acronym like "AI")
                if (!char.IsUpper(result[result.Length - 1]))
                    result.Append(' ');
            }
            result.Append(c);
        }
        return result.ToString();
    }
}