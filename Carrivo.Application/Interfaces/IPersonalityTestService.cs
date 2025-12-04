using Carrivo.Application.DTOs.Common;
using Carrivo.Application.DTOs.Personality_Test;


namespace Carrivo.Application.Interfaces;

public interface IPersonalityTestService
{
    Task<ApiResponse<bool>> SaveTestProgressAsync(SaveTestProgressRequest request);
    Task<ApiResponse<TestResultDto>> SubmitTestAsync(SubmitTestRequest request);
    Task<ApiResponse<TestProgressDto>> GetTestProgressAsync(Guid userId);
}