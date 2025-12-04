using Carrivo.Application.DTOs.Common;
using Carrivo.Application.DTOs.Mentor_System;

namespace Carrivo.Application.Interfaces;

public interface IMentorService
{
    Task<ApiResponse<MentorListDto>> GetMentorsAsync(
        string? search = null,
        string? sort = null,
        string? experience = null,
        string? availability = null,
        decimal? rating = null,
        decimal? price = null);

    Task<ApiResponse<MentorDetailDto>> GetMentorByIdAsync(Guid mentorId);
    Task<ApiResponse<bool>> RequestSessionAsync(RequestSessionRequest request);
}