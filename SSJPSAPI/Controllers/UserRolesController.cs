using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Model;
using SSJPSAPI.DTO;
using System.Data;

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

        // [Route("GetUserRoleById")]
        [HttpGet("{id}")]
        public IActionResult GetUserRoleById(int id)
        {
            var userrole = _context.UserRoles.Find(id);
            return Ok(new
            {
                Data = userrole,
                Status = "200"
            }); ;
        }

        // [Route("PutUserRole")]
        [HttpPut("{id}")]
        public IActionResult PutUserRole(int id, UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            _context.SaveChanges();
            return Ok(new
            {
                Data = "Data Updated Successfully!!",
                Status = "201"
            });
        }

        // [Route("PostUserRole")]
        [HttpPost]
        public IActionResult PostUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
            return Ok(new
            {
                Data = "Data Added Successfully!!",
                Status = "201"
            });
        }

        // [Route("DeleteUserRoleById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUserRoleById(int id)
        {
            var userRole = _context.UserRoles.Find(id);
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
            return Ok(new
            {
                Data = "Data Deleted Successfully!!",
                Status = "204"
            });
        }
    }
}
