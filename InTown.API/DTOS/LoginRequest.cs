using System.ComponentModel.DataAnnotations;

namespace InTown.API.DTOS;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }  = "";
    [Required]
    public string Password { get; set; }  = "";
    
}