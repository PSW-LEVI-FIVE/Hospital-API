using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Feedbacks
{
    public class FeedbackService: IFeedbackService
    {
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            _unitOfWork.FeedbackRepository.Add(feedback);
            _unitOfWork.FeedbackRepository.Save();
            return feedback;
        }

        public Task<IEnumerable<Feedback>> GetAll()
        {
            return _unitOfWork.FeedbackRepository.GetAll();
        }
        
        public Patient getPatientById(int patientId)
        {
           
            Patient p = new Patient();
            p.Id = patientId;
            Patient retPat = _unitOfWork.PatientRepository.GetOne(p.Id);
            return retPat;
        }


        public Feedback ChangePublishmentStatus(Feedback feedback)
        {
            _unitOfWork.FeedbackRepository.Update(feedback);
            _unitOfWork.FeedbackRepository.Save();
            return feedback;
        }
        public Feedback Get(int id)
        {
            return _unitOfWork.FeedbackRepository.GetOne(id);
        }
        public List<ManagersFeedbackDto> GetManagersFeedbacks(IEnumerable<Feedback> feedbacks)
        {
            List<ManagersFeedbackDto> managersFeedbacks = new List<ManagersFeedbackDto>();
            foreach (Feedback feedback in feedbacks)
            {
                Patient patient = getPatientById(feedback.PatientId);
                managersFeedbacks.Add(new ManagersFeedbackDto(feedback.Id,patient.Name + " " + patient.Surname,
                        feedback.FeedbackContent,feedback.AllowPublishment,feedback.Published,feedback.Anonimity));
            }
            return managersFeedbacks;
        }
        public Task<IEnumerable<Feedback>> GetPublished()
        {
            return _unitOfWork.FeedbackRepository.GetPublished();
        }
    }
}