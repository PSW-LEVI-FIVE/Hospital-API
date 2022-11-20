using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<ManagersFeedbackDTO>> GetManagersFeedbacks()
        { 
            List<ManagersFeedbackDTO> managersFeedbacks = new List<ManagersFeedbackDTO>();
            foreach (Feedback feedback in _unitOfWork.FeedbackRepository.GetAll().Result.ToList())
            {
                Patient patient = _unitOfWork.PatientRepository.GetOne(feedback.PatientId);
                managersFeedbacks.Add(new ManagersFeedbackDTO(feedback.Id,patient.Name + " " + patient.Surname, 
                    feedback.FeedbackContent,feedback.AllowPublishment,feedback.Published,feedback.Anonimity));
            }
            return await Task.FromResult(managersFeedbacks);
        }

        public async Task<IEnumerable<PublishedFeedbackDTO>> GetPublishedFeedbacks()
        {
            List<PublishedFeedbackDTO> publishedFeedbacks = new List<PublishedFeedbackDTO>();
            foreach (Feedback feedback in _unitOfWork.FeedbackRepository.GetAll().Result.ToList())
            {
                Patient patient = _unitOfWork.PatientRepository.GetOne(feedback.PatientId);
                if (feedback.Published) 
                    publishedFeedbacks.Add(new PublishedFeedbackDTO(patient.Name + " " + patient.Surname,feedback.FeedbackContent,feedback.Anonimity));
            }
            return await Task.FromResult(publishedFeedbacks);
        }
        public Task<IEnumerable<Feedback>> GetPublished()
        {
            return _unitOfWork.FeedbackRepository.GetPublished();
        }
    }
}