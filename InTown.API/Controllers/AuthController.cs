using System.Security.Claims;
using InTown.API.DTOS;
using InTown.API.Services;
using InTown.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTown.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly TokenService _tokenService;
    public AuthController(IAuthService authService, TokenService
        tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = await _authService.Register(request.Name, request.Email,
            request.Password);
        if (user == null)
            return BadRequest("That email is already used");
        return Ok();
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _authService.Login(request.Email, request.Password);
        if (user == null)
            return Unauthorized("Wrong email or password");
        var response = new LoginResponse();
        response.Token = _tokenService.CreateToken(user);
        response.UserId = user.Id;
        response.Name = user.Name;
        return Ok(response);
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        //User = what´s written on the wristband (not your User Model)
        string id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        string name = User.FindFirst(ClaimTypes.Name)!.Value;
        
        return Ok(new { Id = id, Name = name });
    }

}