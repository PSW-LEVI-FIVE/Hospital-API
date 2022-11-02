using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            Console.WriteLine(p.Id);
            Patient retPat = _unitOfWork.PatientRepository.GetOne(p.Id);
            Console.WriteLine(retPat.Name);
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

        public Task<IEnumerable<Feedback>> GetPublished()
        {
            return _unitOfWork.FeedbackRepository.GetPublished();
        }
    }
}