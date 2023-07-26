using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs.AuthDTO;

public class RegisterRequestDto
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    public string[] Roles { get; set; } = null!;
}
