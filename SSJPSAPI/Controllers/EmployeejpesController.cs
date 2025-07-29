using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;
using System.Data;
using System.IO;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeejpesController : ControllerBase
    {
        private readonly IEmployeejpe _employeejpe;

        public EmployeejpesController(IEmployeejpe employeejpe)
        {
           _employeejpe = employeejpe;
        }


        [HttpGet]
        public IActionResult GetEmployeejpes()
        {
            var data = _employeejpe.GetAll();
            return Ok(new
            {
                Data = data,
                Status = "200"
            });
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(int employeeId)
        {
            var employee = await _employeejpe.GetByEmployeeIdAsync(employeeId);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Employeejpe model, IFormFile? imageFile)
        {
            var result = await _employeejpe.CreateAsync(model, imageFile);
            return Ok(result);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> Update(int employeeId)
        {
            var result = await _employeejpe.UpdateAsync(employeeId, Request.Form, Request.Form.Files.FirstOrDefault());
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _employeejpe.DeleteAsync(id);
            if (!success) return NotFound();

            return Ok(new
            {
                Data = "Data Deleted Successfully!!",
                Status = "204"
            });
        }
    }
}
