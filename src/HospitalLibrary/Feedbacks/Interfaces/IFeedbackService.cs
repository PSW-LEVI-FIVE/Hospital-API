using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Feedbacks.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAll();

        Feedback Create(Feedback Feedback);

        Feedback ChangePublishmentStatus(Feedback Feedback);

        Feedback Get(int id);

        Task<IEnumerable<Feedback>> GetPublished();

    }
}