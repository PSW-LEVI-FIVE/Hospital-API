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

        public async Task<IEnumerable<Feedback>> GetPublished()
        {
            return await _dataContext.Feedbacks.Where(f => f.FeedbackStatus.GetPublished())
                .Include(f => f.FeedbackStatus).ToListAsync();
        }
    }
}