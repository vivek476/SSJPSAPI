using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostjobsController : ControllerBase
    {
        private readonly IPostjob _postjob;

        public PostjobsController(IPostjob postjob)
        {
            _postjob = postjob;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Postjob>>> GetPostjobs()
        {
            var jobs = await _postjob.GetAllAsync();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Postjob>> GetPostjob(int id)
        {
            var job = await _postjob.GetByIdAsync(id);
            if (job == null)
                return NotFound();

            return Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult<Postjob>> PostPostjob(Postjob postjob)
        {
            var created = await _postjob.AddAsync(postjob);
            return CreatedAtAction(nameof(GetPostjob), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> PutPostjob(Postjob postjob)
        {
            var success = await _postjob.UpdateAsync(postjob);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostjob(int id)
        {
            var deleted = await _postjob.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
