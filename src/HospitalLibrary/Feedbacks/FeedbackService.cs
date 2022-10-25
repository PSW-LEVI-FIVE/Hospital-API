using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Interfaces;
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
        public Task<IEnumerable<Feedback>> GetAll()
        {
            return _unitOfWork.FeedbackRepository.GetAll();
        }
    }
}