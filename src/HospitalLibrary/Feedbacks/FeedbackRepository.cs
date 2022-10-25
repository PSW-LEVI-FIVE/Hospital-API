using System.Collections.Generic;
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
        
        public async Task<IEnumerable<Feedback>> FindAll()
        {
            return await _dataContext.Feedbacks.ToListAsync();
        }
    }
}