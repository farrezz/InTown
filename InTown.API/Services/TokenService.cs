using System.Security.Claims;
using InTown.Core.Interfaces;
using InTown.Core.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace InTown.API.Services;

// Makes the login "wristband": a signed string that says who the user is and when it expires
public class TokenService
{
    // Reads values from appsettings.json (like the secret key)
    private readonly IConfiguration _config;
    
    
    // ASP.NET gives us the configuration reader when it creates this service
    public  TokenService(IConfiguration config)
    {
        _config = config;
    }

    // Builds and returns the token string for one user
    public string CreateToken(User user)
    {
        // Start a list of facts to write on the wristband
        var claims = new List<Claim>();
        
        // Fact 1: the user's id, so the API knows who is asking later
        claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
        
        // Fact 2: the user's name, for display
        claims.Add(new Claim(ClaimTypes.Name, user.Name));

        // The secret key from appsettings.json, turned into bytes
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        
        // The stamp: signs the token with the key, using HMAC SHA-256
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Assemble the token: who made it, who it's for, the facts, the expiry and the stamp
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],          // who made the wristband (our app)
            audience: _config["Jwt:Audience"],      // who it's meant for (the React app)
            claims: claims,                         // the facts from above
            expires: DateTime.UtcNow.AddDays(7),    // stops working after 7 days
            signingCredentials: creds);             // the signature stamp
        
        // Convert the token object into the text string React will store and send back
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}