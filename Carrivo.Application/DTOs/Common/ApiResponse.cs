namespace Carrivo.Application.DTOs.Common;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<string>? Errors { get; set; }
    public int StatusCode { get; set; }

    public static ApiResponse<T> Success(T data, string message = "Operation successful", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Data = data,
            IsSuccess = true,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static ApiResponse<T> Failure(string message, IEnumerable<string>? errors = null, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }
}
