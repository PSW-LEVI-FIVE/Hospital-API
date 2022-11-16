using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.AnnualLeaves.Interfaces
{
    public interface IAnnualLeaveRepository : IBaseRepository<AnnualLeave>
    {
        IEnumerable<AnnualLeave> GetAllByDoctorId(int doctorId);
    }
}