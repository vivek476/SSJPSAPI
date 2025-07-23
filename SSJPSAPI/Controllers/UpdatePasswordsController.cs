using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSJPSAPI.DTO;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatePasswordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UpdatePasswordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("Change")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.NewPassword))
            {
                return BadRequest(new { success = false, message = "All fields are required." });
            }

            var user = _context.Users.FirstOrDefault(e => e.Email == model.Email);

            if (user == null)
            {
                return NotFound(new { success = false, message = "User not found." });
            }

            if (user.Password != model.OldPassword)
            {
                return BadRequest(new { success = false, message = "Incorrect old password." });
            }

            user.Password = model.NewPassword;
            _context.SaveChanges();

            return Ok(new { success = true, message = "Password updated successfully." });
        }
    }
}
