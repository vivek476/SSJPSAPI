using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Repository
{
    public class EmployeejpeRepository : IEmployeejpe
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EmployeejpeRepository(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IEnumerable<Employeejpe> GetAll()
        {
            return _context.Employeejpes.ToList();
        }

        public async Task<Employeejpe?> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Employeejpes.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<Employeejpe> CreateAsync(Employeejpe model, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
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
            return model;
        }

        public async Task<Employeejpe?> UpdateAsync(int employeeId, IFormCollection form, IFormFile? file)
        {
            var employee = await _context.Employeejpes.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee == null)
                return null;

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

            if (file != null && file.Length > 0)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadDir, uniqueName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                employee.ImageUrl = Path.Combine("Uploads", uniqueName).Replace("\\", "/");
            }

            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employeejpes.FindAsync(id);
            if (employee == null)
                return false;

            _context.Employeejpes.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
