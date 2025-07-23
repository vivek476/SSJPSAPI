using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSJPSAPI.Model;
using System.Data;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeejpesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeejpesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEmployeejpe() {
            var employeejpe = _context.Employeejpes.ToList();
            return Ok(new
            {
                Data = employeejpe,
                Status = "200"
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeejpeById(int id) { 
            var employeejpes = _context.Employeejpes.Find(id);
            return Ok(new
            {
                Data = employeejpes,
                Status = "200"
            });
        }

        [HttpPut("{id}")]
        public IActionResult PutEmployeejpe(int id, Employeejpe employeejpe) {
            _context.Employeejpes.Update(employeejpe);
            _context.SaveChanges();
            return Ok(new
            {
                Data = "Data Updated Successfully!!",
                Status = "201"
            });
        }

        [HttpPost]
        public IActionResult PostEmployeejpe(Employeejpe employeejpe) {
            _context.Employeejpes.Add(employeejpe);
            _context.SaveChanges();

            return Ok(new
            {
                Data = "Data Added Successfully!!",
                Status = "201"
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeejpeById(int id) {
            var employeejpe = _context.Employeejpes.Find(id);
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
