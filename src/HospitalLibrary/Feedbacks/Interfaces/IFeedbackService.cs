using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Feedbacks.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAll();

        Feedback Create(Feedback Feedback);

        Feedback Update(Feedback Feedback);

        Feedback Get(int id);

        Feedback Update(Feedback NewFeedback);

    }
}