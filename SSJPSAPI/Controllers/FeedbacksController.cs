using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context) 
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostFeedbacks([FromBody] Feedback feedback) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return Ok(feedback);
        }

        [HttpGet]
        public IActionResult GetFeedbacks() {
            var feedbacks = _context.Feedbacks.ToList();
            return Ok(new 
            {
                Data = feedbacks,
                Status = 200,
            });
        }

    }
}
