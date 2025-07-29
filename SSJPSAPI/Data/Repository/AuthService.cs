using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Services;
using System.Security.Claims;

namespace SSJPSAPI.Data.Repository
{
    public class AuthService : IAuthService
    {
        public AuthenticationProperties GetFacebookAuthProperties(string redirectUrl)
        {
            return new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            };
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> GetFacebookClaimsAsync(HttpContext context)
        {
            var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded || result.Principal == null)
            {
                return null;
            }

            var claims = result.Principal.Identities
                .FirstOrDefault()?
                .Claims
                .Select(c => new KeyValuePair<string, string>(c.Type, c.Value));

            return claims;
        }
    }
}
