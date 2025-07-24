using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Model;
using System.Data;
using System.IO;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeejpesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EmployeejpesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult GetEmployeejpe()
        {
            var employeejpe = _context.Employeejpes.ToList();
            return Ok(new
            {
                Data = employeejpe,
                Status = "200"
            });
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(int employeeId)
        {
            var employee = await _context.Employeejpes.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> Update(int employeeId, [FromForm] Employeejpe updated, IFormFile imageFile)
        {
            var employee = await _context.Employeejpes.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee == null) return NotFound();

            if (imageFile != null)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(uploadDir, uniqueName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                employee.ImageUrl = Path.Combine("Uploads", uniqueName).Replace("\\", "/");
            }

            // Update all fields
            employee.Firstname = updated.Firstname;
            employee.Middlename = updated.Middlename;
            employee.Lastname = updated.Lastname;
            employee.Address = updated.Address;
            employee.City = updated.City;
            employee.Pincode = updated.Pincode;
            employee.Mobile = updated.Mobile;
            employee.Degree = updated.Degree;
            employee.Skill = updated.Skill;
            employee.Passyear = updated.Passyear;
            employee.Experience = updated.Experience;
            employee.Detail = updated.Detail;

            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Employeejpe model, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(uploadDir, uniqueName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                model.ImageUrl = Path.Combine("Uploads", uniqueName).Replace("\\", "/");
            }

            _context.Employeejpes.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeejpeById(int id)
        {
            var employeejpe = _context.Employeejpes.Find(id);
            if (employeejpe == null)
                return NotFound();

            _context.Employeejpes.Remove(employeejpe);
            _context.SaveChanges();

            return Ok(new
            {
                Data = "Data Deleted Successfully!!",
                Status = "204"
            });
        }
    }
}
