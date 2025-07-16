using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ExperiencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // [Route("GetExperience")]
        [HttpGet]
        public IActionResult GetExperience()
        {
            return Ok(_context.Experiences.ToList());
        }

        // [Route("GetExperienceById")]
        [HttpGet("{id}")]
        public IActionResult GetExperienceById(int id)
        {
            return Ok(_context.Experiences.Find(id));
        }

        // [Route("PutExperience")]
        [HttpPut("{id}")]
        public IActionResult PutExperience(int id, Experience experience)
        {
            _context.Experiences.Update(experience);
            _context.SaveChanges();
            return Ok("Data Updated Successfully!!");
        }

        // [Route("PostExperience")]
        [HttpPost]
        public IActionResult PostExperience(Experience experience)
        {
            _context.Experiences.Add(experience);
            _context.SaveChanges();
            return Ok("Data Added Successfully!!");
        }

        // [Route("DeleteExperienceById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteExperienceById(int id)
        {
            var experience = _context.Experiences.Find(id);
            _context.Experiences.Remove(experience);
            _context.SaveChanges();
            return Ok("Data Deleted Successfully!!");
        }
    }
}