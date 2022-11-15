using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.AnnualLeaves.Interfaces
{
    public interface IAnnualLeaveService
    {
        Task<IEnumerable<AnnualLeave>> GetAllByDoctorId(int doctorId);

        AnnualLeave Create(AnnualLeave annualLeave);
    }
}