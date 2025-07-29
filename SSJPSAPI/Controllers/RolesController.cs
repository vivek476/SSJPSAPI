using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Data.Repository;
using SSJPSAPI.Model;
using System.Data;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRole _role;

        public RolesController(IRole role)
        {
            _role = role;
        }

        // [Route("GetRole")]
        [HttpGet]
        public IActionResult GetRole()
        {
            var roles = _role.GetRoles();
            return Ok(new { Data = roles, Status = "200" });
        }

        // [Route("GetRoleById")]
        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            var role = _role.GetRoleById(id);
            return Ok(new { Data = role, Status = "200" });
        }

        // [Route("PutRole")]
        [HttpPut("{id}")]
        public IActionResult PutRole(int id, Role role)
        {
            _role.UpdateRole(id, role);
            return Ok(new { Data = "Data Updated Successfully!!", Status = "201" });
        }


        // [Route("PostRole")]
        [HttpPost]
        public IActionResult PostRole(Role role)
        {
            _role.AddRole(role);
            return Ok(new { Data = "Data Added Successfully!!", Status = "201" });
        }

        // [Route("DeleteRoleById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRoleById(int id)
        {
            _role.DeleteRole(id);
            return Ok(new { Data = "Data Deleted Successfully!!", Status = "204" });
        }
    }
}
