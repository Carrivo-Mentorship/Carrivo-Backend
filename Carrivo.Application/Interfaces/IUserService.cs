using Carrivo.Application.DTOs.Common;

using Carrivo.Application.DTOs.User_Profile;

namespace Carrivo.Application.Interfaces;

public interface IUserService
{
    Task<ApiResponse<UserProfileDto>> GetUserProfileAsync(Guid userId);
    Task<ApiResponse<CurrentPathDto>> GetCurrentPathAsync(Guid userId);
    Task<ApiResponse<PathOverviewDto>> GetPathOverviewAsync(Guid userId);
    Task<ApiResponse<UserMentorDto>> GetUserMentorAsync(Guid userId);
    Task<ApiResponse<QuickAccessDto>> GetQuickAccessAsync(Guid userId);
    Task<ApiResponse<NotificationCountDto>> GetNotificationCountAsync(Guid userId);
}