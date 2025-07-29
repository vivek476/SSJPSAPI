using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyjpcsController : ControllerBase
    {
        private readonly ICompanyjpc _companyjpc;

        public CompanyjpcsController(ICompanyjpc companyjpc)
        {
            _companyjpc = companyjpc;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyjpcs()
        {
            var data = await _companyjpc.GetAllAsync();
            return Ok(new
            {
                Data = data,
                Status = 200
            });
        }

        [HttpGet("by-employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(string employeeId)
        {
            var company = await _companyjpc.GetByEmployeeIdAsync(employeeId);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] Companyjpc company)
        {
            if (company == null || string.IsNullOrEmpty(company.EmployeeId))
                return BadRequest("Invalid company data or missing employeeId.");

            var created = await _companyjpc.AddAsync(company);
            return Ok(created);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateCompany(string employeeId, [FromBody] Companyjpc updated)
        {
            var result = await _companyjpc.UpdateAsync(employeeId, updated);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
