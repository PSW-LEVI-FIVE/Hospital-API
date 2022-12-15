using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Patients;

namespace HospitalLibrary.Feedbacks.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAll();
        Task<Feedback> Create(Feedback Feedback);
        Feedback ChangePublishmentStatus(int feedbackId);
        Task<IEnumerable<PublishedFeedbackDTO>> GetPublishedFeedbacks();
        public Task<IEnumerable<ManagersFeedbackDTO>> GetManagersFeedbacks();
        Feedback Get(int id);

    }
}