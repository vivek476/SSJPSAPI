using Microsoft.IdentityModel.Tokens;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.DTO;
using SSJPSAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SSJPSAPI.Data.Repository
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public UserRepository(ApplicationDbContext context, IConfiguration configuration, IJwtService jwtService)
        {
            _context = context;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public IEnumerable<User> GetUser()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public (string Token, User User, Role Role)? Login(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (user == null) return null;

            var userRole = _context.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
            var role = _context.Roles.FirstOrDefault(r => r.Id == userRole.RoleId);

            var token = _jwtService.GenerateJwtToken(user);
            return (token, user, role);
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

        public string Signup(SignupRequest request)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null)
                return "Email Already Registered.";

            var newUser = new User
            {
                FullName = request.FullName,
                Mobile = request.Mobile,
                Email = request.Email,
                Password = request.Password,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return "Signup Successfully!!";
        }

        public void UpdateUser(int id, User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
