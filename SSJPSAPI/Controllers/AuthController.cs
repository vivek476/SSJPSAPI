using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSJPSAPI.Data.Interface;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("login-facebook")]
        public IActionResult LoginWithFacebook()
        {
            var redirectUrl = Url.Action("FacebookResponse", "Auth");
            var properties = _authService.GetFacebookAuthProperties(redirectUrl);
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        [HttpGet("facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            var claims = await _authService.GetFacebookClaimsAsync(HttpContext);
            if (claims == null)
            {
                return BadRequest("Authentication failed. Facebook login was not successful.");
            }

            string name = claims.FirstOrDefault(c => c.Key.Contains("name")).Value;
            string email = claims.FirstOrDefault(c => c.Key.Contains("email")).Value;
            string role = "Employee"; // default role for Facebook login

            // ✅ Null check
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                return BadRequest("Facebook did not return valid name or email.");
            }

            // ✅ Safe escaping
            var queryString = $"name={Uri.EscapeDataString(name)}&email={Uri.EscapeDataString(email)}&role={Uri.EscapeDataString(role)}";

            var redirectUrl = $"http://localhost:3000/facebook-callback?{queryString}";
            return Redirect(redirectUrl);
        }


    }
}
