using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.AnnualLeaves
{
    public class AnnualLeaveRepository : BaseRepository<AnnualLeave>, IAnnualLeaveRepository
    {
        public AnnualLeaveRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}