using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CryptoApp.Data;
using CryptoApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CryptoApp.Business
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repository;

        public AuthService(IConfiguration configuration, IUserRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public User CheckLogin(string email, string pasword)
        {
            User user = null;
            foreach (var userLogin in _repository.GetAllUsers())
            {
                if (userLogin.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                    userLogin.Password.Equals(pasword))
                {
                    user = userLogin;
                }
            }
            return user;
        }

        public string GenerateJwtToken(User user)
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

        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user) 
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId)) 
            { 
                return false; 
            }
            var isOwnResource = userId == requestedUserID;

            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var isAdmin = roleClaim != null && roleClaim.Value == Roles.Admin;
            
            var hasAccess = isOwnResource || isAdmin;
            return hasAccess;
        }
     
    }
}
