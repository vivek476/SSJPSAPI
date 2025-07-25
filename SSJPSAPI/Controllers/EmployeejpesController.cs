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
        public async Task<IActionResult> Update(int employeeId)
        {
            var employee = await _context.Employeejpes.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee == null)
                return NotFound();

            var form = Request.Form;

            // Update fields from form
            employee.Firstname = form["Firstname"];
            employee.Middlename = form["Middlename"];
            employee.Lastname = form["Lastname"];
            employee.Address = form["Address"];
            employee.City = form["City"];
            employee.Pincode = form["Pincode"];
            employee.Mobile = form["Mobile"];
            employee.Degree = form["Degree"];
            employee.Skill = form["Skill"];
            employee.Passyear = form["Passyear"];
            employee.Experience = form["Experience"];
            employee.Detail = form["Detail"];

            // Handle Image Upload
            var file = form.Files.FirstOrDefault();
            if (file != null && file.Length > 0)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadDir, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                employee.ImageUrl = Path.Combine("Uploads", uniqueName).Replace("\\", "/");
            }

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
