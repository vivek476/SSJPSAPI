using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Interface
{
    public interface IFeedback
    {
        IEnumerable<Feedback> GetAllFeedbacks();
        void AddFeedback(Feedback feedback);
    }
}
