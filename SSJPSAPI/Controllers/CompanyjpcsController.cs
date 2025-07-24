using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyjpcsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompanyjpcsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET Company by EmployeeId
        [HttpGet("by-employee/{employeeId}")]
        public async Task<IActionResult> GetCompanyByEmployeeId(string employeeId)
        {
            var company = await _context.Companyjpcs.FirstOrDefaultAsync(c => c.EmployeeId == employeeId);
            if (company == null)
                return NotFound();
            return Ok(company);
        }

        // ✅ POST Add new company (ensure employeeId is provided)
        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] Companyjpc company)
        {
            if (company == null || string.IsNullOrEmpty(company.EmployeeId))
                return BadRequest("Invalid company data or missing employeeId.");

            _context.Companyjpcs.Add(company);
            await _context.SaveChangesAsync();
            return Ok(company);
        }

        // ✅ PUT Update company by employeeId
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateCompany(string employeeId, [FromBody] Companyjpc updated)
        {
            var company = await _context.Companyjpcs.FirstOrDefaultAsync(c => c.EmployeeId == employeeId);
            if (company == null) return NotFound();

            company.CompanyName = updated.CompanyName;
            company.Address = updated.Address;
            company.City = updated.City;
            company.Pincode = updated.Pincode;
            company.Mobile = updated.Mobile;
            company.Email = updated.Email;
            company.ContactPerson = updated.ContactPerson;
            company.Detail = updated.Detail;

            await _context.SaveChangesAsync();
            return Ok(company);
        }
    }
}
