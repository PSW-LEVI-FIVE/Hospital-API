using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Feedbacks
{
    public class FeedbackService: IFeedbackService
    {
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Feedback NewFeedback)
        {
            _unitOfWork.FeedbackRepository.Add(NewFeedback);
            _unitOfWork.FeedbackRepository.Save();
        }

        public Task<IEnumerable<Feedback>> GetAll()
        {
            return _unitOfWork.FeedbackRepository.GetAll();
        }

    }
}