using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.DTO;
using SSJPSAPI.Model;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }

        // [Route("GetUser")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var users = _user.GetUser();
            return Ok(new { Data = users, Status = "200" });
        }

        // [Route("GetUserById")]
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _user.GetUserById(id);
            return Ok(new { Data = user, Status = "200" });
        }

        // [Route("PutUser")]
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            _user.UpdateUser(id, user);
            return Ok(new { Data = "Data Updated Successfully!!", Status = "201" });
        }

        // [Route("PostUser")]
        [HttpPost("signup")]
        public IActionResult Signup(SignupRequest request)
        {
            var result = _user.Signup(request);
            if (result == "Email Already Registered.")
                return BadRequest(result);

            return Ok(new { Data = result, Status = "201" });
        }

        [HttpPost("login")]
        public IActionResult Login( LoginRequest request)
        {
            var loginResult = _user.Login(request);
            if (loginResult == null)
                return Unauthorized("Invalid Email Or Password.");

            return Ok(new
            {
                Token = loginResult.Value.Token,
                User = loginResult.Value.User,
                Role = loginResult.Value.Role,
                Status = "200"
            });
        }

        // [Route("DeleteUserById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            _user.DeleteUser(id);
            return Ok(new { Data = "Data Deleted Successfully!!", Status = "204" });
        }
    }
}