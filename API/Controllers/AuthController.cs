using Microsoft.AspNetCore.Mvc;

using CryptoApp.Business;
using CryptoApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CryptoApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
      private readonly IConfiguration _configuration;

    public AuthController(ILogger<UsersController> logger, IUserService userService, IConfiguration configuration)
    {
        _logger = logger;
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("Login/")]
    public IActionResult Login([FromBody] UserLoginDTO userLoginDTO)
    {
        try
        {
            var user = _userService.CheckLogin(userLoginDTO.Email, userLoginDTO.Password);
            if (user != null)
            {
                var token = GenerateJwtToken(user);
                return Ok(token);
            }
            else
            {
                return NotFound("No se ha encontrado ningún usuario con esas credenciales");
            }
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado ningún usuario. {knfex.Message}");
           return NotFound($"No se ha encontrado ningún usuario. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"No se ha encontrado ningún usuario. {ex.Message}");
            return BadRequest($"No se ha encontrado ningún usuario. {ex.Message}");
        }
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = _configuration["JWT:SecretKey"]; 
        var key = Encoding.ASCII.GetBytes(secretKey); 

        var tokenDescriptor = new SecurityTokenDescriptor
        {
           Subject = new ClaimsIdentity(new Claim[]
           {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
           }),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) 
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /*public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user) 
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId)) 
        { 
            return false; 
        }
        var isOwnResource = userId == requestedUserID;

        var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (roleClaim != null) return false;
        var isAdmin = roleClaim!.Value == Roles.Admin;
        
        var hasAccess = isOwnResource || isAdmin;
        return hasAccess;
    }*/

}
