using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Model;

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
            return Ok(_context.Roles.ToList());
        }

        // [Route("GetRoleById")]
        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            return Ok(_context.Roles.Find(id));
        }

        // [Route("PutRole")]
        [HttpPut("{id}")]
        public IActionResult PutRole(int id, Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return Ok("Data Updated Succesfully!!");
        }

        // [Route("PostRole")]
        [HttpPost]
        public IActionResult PostRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();

            return Ok("Data Added Successfully!!");
        }

        // [Route("DeleteRoleById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRoleById(int id)
        {
            var role =  _context.Roles.Find(id);
            _context.Roles.Remove(role);
            _context.SaveChanges();

            return Ok("Data Deleted Successfully!!");
        }
    }
}
