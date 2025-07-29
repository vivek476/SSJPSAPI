using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;

namespace SSJPSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedback _feedback;

        public FeedbacksController(IFeedback feedback) 
        {
            _feedback = feedback;
        }

        [HttpPost]
        public IActionResult PostFeedbacks([FromBody] Feedback feedback)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _feedback.AddFeedback(feedback);
            return Ok(new
            {
                Message = "Feedback submitted successfully",
                Data = feedback,
                Status = 200
            });
        }

        [HttpGet]
        public IActionResult GetFeedbacks()
        {
            var feedbacks = _feedback.GetAllFeedbacks();
            return Ok(new
            {
                Data = feedbacks,
                Status = 200
            });
        }

    }
}
