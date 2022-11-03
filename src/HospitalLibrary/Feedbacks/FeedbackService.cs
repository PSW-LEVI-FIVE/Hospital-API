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
        public async Task<IEnumerable<ManagersFeedbackDto>> GetManagersFeedbacks()
        { 
            List<Feedback> feedbacks = Task.Run(() => _unitOfWork.FeedbackRepository.GetAll()).Result.ToList();
            List<ManagersFeedbackDto> managersFeedbacks = new List<ManagersFeedbackDto>();
            foreach (Feedback feedback in feedbacks)
            {
                Patient patient = getPatientById(feedback.PatientId);
                managersFeedbacks.Add(new ManagersFeedbackDto(feedback.Id,patient.Name + " " + patient.Surname,
                        feedback.FeedbackContent,feedback.AllowPublishment,feedback.Published,feedback.Anonimity));
            }
            return await Task.FromResult(managersFeedbacks);
        }

        public async Task<IEnumerable<PublishedFeedbackDto>> GetPublishedFeedbacks()
        {
            List<PublishedFeedbackDto> publishedFeedbacks = new List<PublishedFeedbackDto>();
            foreach (Feedback feedback in _unitOfWork.FeedbackRepository.GetAll().Result.ToList())
            {
                Patient patient = _unitOfWork.PatientRepository.GetOne(feedback.PatientId);
                if (feedback.Published) 
                    publishedFeedbacks.Add(new PublishedFeedbackDto(patient.Name + " " + patient.Surname,feedback.FeedbackContent,feedback.Anonimity));
            }
            return await Task.FromResult(publishedFeedbacks);
        }
        public Task<IEnumerable<Feedback>> GetPublished()
        {
            return _unitOfWork.FeedbackRepository.GetPublished();
        }
    }
}