using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Model;
using SSJPSAPI.DTO;

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

        // ✅ Get All UserRoles with FullName and RoleName
        [HttpGet]
        public IActionResult GetUserRole()
        {
            var userroles = (from ur in _context.UserRoles
                             join u in _context.Users on ur.UserId equals u.Id
                             join r in _context.Roles on ur.RoleId equals r.Id
                             select new UserRoleDto
                             {
                                 Id = ur.Id,
                                 UserId = ur.UserId,
                                 FullName = u.FullName,
                                 RoleId = ur.RoleId,
                                 RoleName = r.Name
                             }).ToList();

            return Ok(new
            {
                Data = userroles,
                Status = "200"
            });
        }

        // ✅ Get UserRole by ID
        [HttpGet("{id}")]
        public IActionResult GetUserRoleById(int id)
        {
            var userrole = _context.UserRoles.Find(id);

            if (userrole == null)
            {
                return NotFound(new
                {
                    Message = $"UserRole with ID {id} not found",
                    Status = "404"
                });
            }

            return Ok(new
            {
                Data = userrole,
                Status = "200"
            });
        }

        // ✅ Update UserRole (Edit)
        [HttpPut("{id}")]
        public IActionResult PutUserRole(int id, UserRole userRole)
        {
            if (id != userRole.Id)
            {
                return BadRequest(new
                {
                    Message = "ID mismatch between route and body",
                    Status = "400"
                });
            }

            var existing = _context.UserRoles.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (existing == null)
            {
                return NotFound(new
                {
                    Message = $"UserRole with ID {id} not found",
                    Status = "404"
                });
            }

            _context.UserRoles.Update(userRole);
            _context.SaveChanges();

            return NoContent(); // 204 response for successful update without body
        }

        // ✅ Create UserRole (Assign)
        [HttpPost]
        public IActionResult PostUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            return StatusCode(201, new
            {
                Data = "Data Added Successfully!!",
                Status = "201"
            });
        }

        // ✅ Delete UserRole
        [HttpDelete("{id}")]
        public IActionResult DeleteUserRoleById(int id)
        {
            var userRole = _context.UserRoles.Find(id);

            if (userRole == null)
            {
                return NotFound(new
                {
                    Message = $"UserRole with ID {id} not found",
                    Status = "404"
                });
            }

            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

            return NoContent(); // 204 status
        }
    }
}
