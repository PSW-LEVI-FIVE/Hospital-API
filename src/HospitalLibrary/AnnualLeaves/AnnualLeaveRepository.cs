using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public IEnumerable<AnnualLeave> GetAllByDoctorId(int doctorId)
        {
            return  _dataContext.AnnualLeaves
                .Where(al => al.State != AnnualLeaveState.DELETED)
                .Where(al =>al.DoctorId == doctorId)
                .OrderBy(al => al.StartAt)
                .ToList();
        }
    }
}