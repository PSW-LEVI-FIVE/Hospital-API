using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.AnnualLeaves.Interfaces
{
    public interface IAnnualLeaveService
    {
        IEnumerable<AnnualLeave> GetAllByDoctorId(int doctorId);

        Task<AnnualLeave> Create(AnnualLeave annualLeave);
    }
}