using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Patients;

namespace HospitalLibrary.Feedbacks.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAll();
        Feedback Create(Feedback Feedback);
        Feedback ChangePublishmentStatus(Feedback Feedback);
        Task<IEnumerable<PublishedFeedbackDTO>> GetPublishedFeedbacks();
        public Task<IEnumerable<ManagersFeedbackDTO>> GetManagersFeedbacks();
        Feedback Get(int id);
        Task<IEnumerable<Feedback>> GetPublished();

    }
}