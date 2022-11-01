using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Feedbacks.Interfaces
{
    public interface IFeedbackRepository: IBaseRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetPublished();
    }
}