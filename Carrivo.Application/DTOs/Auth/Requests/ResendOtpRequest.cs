using System.ComponentModel.DataAnnotations;

namespace Carrivo.Application.DTOs.Auth.Requests;

public class ResendOtpRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
}
