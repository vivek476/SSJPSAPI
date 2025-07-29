using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.DTO;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRole _userRoleRepo;

        public UserRolesController(IUserRole userRoleRepo)
        {
            _userRoleRepo = userRoleRepo;
        }

        // ✅ Get All UserRoles with FullName and RoleName
        [HttpGet]
        public IActionResult GetUserRole()
        {
            var userRoles = _userRoleRepo.GetAllUserRoles();
            return Ok(new
            {
                Data = userRoles,
                Status = "200"
            });
        }

        // ✅ Get UserRole by ID
        [HttpGet("{id}")]
        public IActionResult GetUserRoleById(int id)
        {
            var userRole = _userRoleRepo.GetUserRoleById(id);
            if (userRole == null)
            {
                return NotFound(new
                {
                    Message = $"UserRole with ID {id} not found",
                    Status = "404"
                });
            }

            return Ok(new
            {
                Data = userRole,
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

            var existing = _userRoleRepo.GetUserRoleById(userRole.Id);
            if (existing == null)
            {
                return NotFound(new
                {
                    Message = $"UserRole with ID {id} not found",
                    Status = "404"
                });
            }

            _userRoleRepo.UpdateUserRole(userRole);
            return NoContent(); // 204
        }

        // ✅ Create UserRole (Assign)
        [HttpPost]
        public IActionResult PostUserRole(UserRole userRole)
        {
            _userRoleRepo.CreateUserRole(userRole);
            return StatusCode(201, new
            {
                Message = "UserRole created successfully",
                Status = "201"
            });
        }

        // ✅ Delete UserRole
        [HttpDelete("{id}")]
        public IActionResult DeleteUserRoleById(int id)
        {
            var existing = _userRoleRepo.GetUserRoleById(id);
            if (existing == null)
            {
                return NotFound(new
                {
                    Message = $"UserRole with ID {id} not found",
                    Status = "404"
                });
            }

            _userRoleRepo.DeleteUserRole(id);
            return NoContent(); // 204
        }
    }
}
