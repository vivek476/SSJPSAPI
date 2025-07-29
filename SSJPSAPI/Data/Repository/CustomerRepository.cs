using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Repository
{
    public class CustomerRepository : ICustomer
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        
        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var exists = await _context.Customers.AnyAsync(c => c.Id == customer.Id);
            if (!exists) return false;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
