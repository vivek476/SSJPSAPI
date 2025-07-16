using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DegreesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[Route("GetDegree")]
        [HttpGet]
        public IActionResult GetDegree()
        {
            return Ok(_context.Degrees.ToList());
        }

        //[Route("GetDegreeById")]
        [HttpGet("{id}")]
        public IActionResult GetDegreeById(int id)
        {
            return Ok(_context.Degrees.Find(id));
        }

        //[Route("PutDegree")]
        [HttpPut("{id}")]
        public IActionResult PutDegree(int id, Degree degree)
        {
            _context.Degrees.Update(degree);
            _context.SaveChanges();
            return Ok("Data Updated Successfully!!");
        }

        //[Route("PostDegree")]
        [HttpPost]
        public IActionResult PostDegree(Degree degree)
        {
            _context.Degrees.Add(degree);
            _context.SaveChanges();
            return Ok("Data Added Successfully!!");
        }

        //[Route("DeleteDegreeById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteDegree(int id)
        {
            var degree = _context.Degrees.Find(id);
            _context.Degrees.Remove(degree);
            _context.SaveChanges();
            return Ok("Data Deleted Successfully!!");
        }
    }
}