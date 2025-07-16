using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // [Route("GetUserRole")]
        [HttpGet]
        public IActionResult GetUserRole()
        {
            return Ok(_context.UserRoles.ToList());
        }

        // [Route("GetUserRoleById")]
        [HttpGet("{id}")]
        public IActionResult GetUserRoleById(int id)
        {
            return Ok(_context.UserRoles.Find(id));
        }

        // [Route("PutUserRole")]
        [HttpPut("{id}")]
        public IActionResult PutUserRole(int id, UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            _context.SaveChanges();
            return Ok("Data Updated Successfully!!");
        }

        // [Route("PostUserRole")]
        [HttpPost]
        public IActionResult PostUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
            return Ok("Data Added Successfully!!");
        }

        // [Route("DeleteUserRoleById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUserRoleById(int id)
        {
            var userRole = _context.UserRoles.Find(id);
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
            return Ok("Data Deleted Successfully!!");
        }
    }
}
