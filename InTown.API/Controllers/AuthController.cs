using InTown.API.DTOS;
using InTown.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InTown.API.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = await _authService.Register(request.Name, request.Email, request.Password);
        if (user == null)
            return BadRequest("That email is already used");
        return Ok();
    }
    
    
}