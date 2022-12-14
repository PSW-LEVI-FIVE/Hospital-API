using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Feedbacks
{
    public class FeedbackRepository: BaseRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public Task<IEnumerable<Feedback>> GetPublished()
        {
            return Task.FromResult(_dataContext.Feedbacks.ToList()
                .Where(f => f.FeedbackStatus.GetPublished()));
        }
    }
}