using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // [Route("GetUser")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var users = _context.Users.ToList();
            return Ok(new
            {
                Data = users,
                Status = "200"
            });
        }

        // [Route("GetUserById")]
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            return Ok(new
            {
                Data = user,
                Status = "200"
            });


        }

        // [Route("PutUser")]
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok(new
            {
                Data = "Data Updated Successfully!!",
                Status = "201"
            });
        }

        // [Route("PostUser")]
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] SignupRequest request)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null)
                return BadRequest("Email Already Registered.");

            var newUser = new User
            {
                FullName = request.FullName,
                Mobile = request.Mobile,
                Email = request.Email,
                Password = request.Password,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(new
            {
                Data = "Signup Successfully!!",
                Status = "201"
            }); 
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (user == null)
                return Unauthorized("Invalid Email Or Password.");

            var userRole = _context.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
            var role = _context.Roles.FirstOrDefault(r => r.Id == userRole.RoleId);

            var token = GenerateJwtToken(user);
            return Ok(new
            {
                Token = token,
                User = user,
                Role = role,
                Status = "200"
            });


        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.FullName),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiresInMinutes"])),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        // [Route("DeleteUserById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(new
            {
                Data = "Data Deleted Successfully!!",
                Status = "204"
            });
        }
    }
}