using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SkillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // [Route("GetSkill")]
        [HttpGet]
        public IActionResult GetSkill()
        {
            return Ok(_context.Skills.ToList());
        }

        // [Route("GetSkillById")]
        [HttpGet("{id}")]
        public IActionResult GetSkillById(int id)
        {
            return Ok(_context.Skills.Find(id));
        }

        // [Route("PutSkill")]
        [HttpPut("{id}")]
        public IActionResult PutSkill(int id, Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
            return Ok("Data Updated Successfully!!");
        }

        // [Route("PostSkill")]
        [HttpPost]
        public IActionResult PostSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return Ok("Data Added Successfully!!");
        }

        // [Route("DeleteSkillById")]
        [HttpDelete("{id}")]
        public IActionResult DeleteSkillById(int id)
        {
            var skill = _context.Skills.Find(id);
            _context.Skills.Remove(skill);
            _context.SaveChanges();
            return Ok("Data Deleted Successfully!!");
        }
    }

}