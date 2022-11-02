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

        public List<AnonymousFeedbackDTO> anonymousList(IEnumerable<Feedback> feedbacks);
        Feedback Get(int id);
        
        public Patient getPatientById(int patientId);

        Task<IEnumerable<Feedback>> GetPublished();

    }
}