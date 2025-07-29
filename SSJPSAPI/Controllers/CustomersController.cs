using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomer _customer;

        public CustomersController(ICustomer customer)
        {
            _customer = customer;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customer.GetAllCustomersAsync();
            return Ok(new { Data = customers, Status = "200" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customer.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound(new { Message = $"Customer with ID {id} not found", Status = "404" });

            return Ok(new { Data = customer, Status = "200" });
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            var created = await _customer.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = created.Id }, new { Message = "Customer created successfully", Status = "201", Data = created });
        }

        [HttpPut]
        public async Task<IActionResult> PutCustomer(Customer customer)
        {
            var updated = await _customer.UpdateCustomerAsync(customer);
            if (!updated)
                return NotFound(new { Message = $"Customer with ID {customer.Id} not found", Status = "404" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customer.DeleteCustomerAsync(id);
            if (!deleted)
                return NotFound(new { Message = $"Customer with ID {id} not found", Status = "404" });

            return NoContent();
        }
    }
}
