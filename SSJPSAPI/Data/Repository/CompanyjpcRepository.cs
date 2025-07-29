using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SSJPSAPI.Data.Repository
{
    public class CompanyjpcRepository : ICompanyjpc 
    {
        private readonly ApplicationDbContext _context;

        public CompanyjpcRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Companyjpc>> GetAllAsync()
        {
            return await _context.Companyjpcs.ToListAsync();
        }

        public async Task<Companyjpc> GetByEmployeeIdAsync(string employeeId)
        {
            return await _context.Companyjpcs.FirstOrDefaultAsync(c => c.EmployeeId == employeeId);
        }

        public async Task<Companyjpc> AddAsync(Companyjpc company)
        {
            _context.Companyjpcs.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<Companyjpc> UpdateAsync(string employeeId, Companyjpc updated)
        {
            var company = await _context.Companyjpcs.FirstOrDefaultAsync(c => c.EmployeeId == employeeId);
            if (company == null) return null;

            company.CompanyName = updated.CompanyName;
            company.Address = updated.Address;
            company.City = updated.City;
            company.Pincode = updated.Pincode;
            company.Mobile = updated.Mobile;
            company.Email = updated.Email;
            company.ContactPerson = updated.ContactPerson;
            company.Detail = updated.Detail;

            await _context.SaveChangesAsync();
            return company;
        }
    }
}
