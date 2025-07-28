using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostjobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostjobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Postjobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Postjob>>> GetPostjobs()
        {
            return await _context.Postjobs.ToListAsync();
        }

        // GET: api/Postjobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Postjob>> GetPostjob(int id)
        {
            var postjob = await _context.Postjobs.FindAsync(id);
            if (postjob == null)
                return NotFound();

            return postjob;
        }

        // POST: api/Postjobs
        [HttpPost]
        public async Task<ActionResult<Postjob>> PostPostjob(Postjob postjob)
        {
            _context.Postjobs.Add(postjob);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostjob), new { id = postjob.Id }, postjob);
        }

        // PUT: api/Postjobs
        [HttpPut]
        public async Task<IActionResult> PutPostjob(Postjob postjob)
        {
            if (!_context.Postjobs.Any(e => e.Id == postjob.Id))
                return NotFound();

            _context.Entry(postjob).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Postjobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostjob(int id)
        {
            var postjob = await _context.Postjobs.FindAsync(id);
            if (postjob == null)
                return NotFound();

            _context.Postjobs.Remove(postjob);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
