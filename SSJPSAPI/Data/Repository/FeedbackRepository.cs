using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace SSJPSAPI.Data.Repository
{
    public class FeedbackRepository : IFeedback
    {
        private readonly ApplicationDbContext _context;
        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Feedback> GetAllFeedbacks()
        {
            return _context.Feedbacks.ToList();
        }
        public void AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }
    }
}
