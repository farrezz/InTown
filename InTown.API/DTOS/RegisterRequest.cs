using System.ComponentModel.DataAnnotations;

namespace InTown.API.DTOS;

public class RegisterRequest
{
    [Required] 
    public string Name { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";
    
    [Required, MinLength(6)]
    public string Password { get; set; }  = "";
    
}