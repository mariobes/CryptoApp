using System.Security.Claims;
using CryptoApp.Models;

namespace CryptoApp.Business
{
    public interface IAuthService
    {
        User CheckLogin(string email, string password);
        public string GenerateJwtToken(User user);
        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);
    }
}
