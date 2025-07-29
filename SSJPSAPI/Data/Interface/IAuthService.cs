using Microsoft.AspNetCore.Authentication;

namespace SSJPSAPI.Data.Interface
{
    public interface IAuthService
    {
        AuthenticationProperties GetFacebookAuthProperties(string redirectUrl);
        Task<IEnumerable<KeyValuePair<string, string>>> GetFacebookClaimsAsync(HttpContext context);
    }
}
