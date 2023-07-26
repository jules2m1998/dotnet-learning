using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs.AuthDTO;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
