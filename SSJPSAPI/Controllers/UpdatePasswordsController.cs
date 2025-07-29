using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.DTO;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatePasswordsController : ControllerBase
    {
        private readonly IUpdatePassword _updatePassword;
        public UpdatePasswordsController(IUpdatePassword updatePassword)
        {
            _updatePassword = updatePassword;
        }


        [HttpPut("Change")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto model)
        {
            var result = _updatePassword.ChangePassword(model, out string message);
            if (!result)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }
    }
}
