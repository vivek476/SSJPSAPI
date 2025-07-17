using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using SSJPSAPI.Model;
using System.Data;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // [Route("GetRole")]
        [HttpGet]
        public IActionResult GetRole()
        {
            var roles = _context.Roles.ToList();
            return Ok(new
            {
                Data = roles,
                Status = "200"
            });
        }

        // [Route("GetRoleById")]
        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            var role = _context.Roles.Find(id);
            return Ok(new
            {
                Data = role,
                Status = "200"
            });
        }

        // [Route("PutRole")]
        [HttpPut("{id}")]
        public IActionResult PutRole(int id, Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return Ok(new
            {
                Data = "Data Updated Successfully!!",
                Status = "201"
            }); 
        }

        // [Route("PostRole")]
        [HttpPost]
        public IActionResult PostRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();

            return Ok(new
            {
                Data = "Data Added Successfully!!",
                Status = "201"
            });
        }

        // [Route("DeleteRoleById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRoleById(int id)
        {
            var role =  _context.Roles.Find(id);
            _context.Roles.Remove(role);
            _context.SaveChanges();

            return Ok(new
            {
                Data = "Data Deleted Successfully!!",
                Status = "204"
            });
        }
    }
}
