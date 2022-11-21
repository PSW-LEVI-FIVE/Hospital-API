using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.AnnualLeaves.Interfaces
{
    public interface IAnnualLeaveRepository : IBaseRepository<AnnualLeave>
    {
        IEnumerable<AnnualLeave> GetAllByDoctorId(int doctorId);
        List<int> GetDoctorsThatHaveAnnualLeaveInRange(TimeInterval range);
    }
}