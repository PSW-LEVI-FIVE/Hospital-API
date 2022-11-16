using System.Threading.Tasks;

namespace HospitalLibrary.AnnualLeaves.Interfaces
{
    public interface IAnnualLeaveValidator
    {
        Task Validate(AnnualLeave annualLeave);
        void CancelValidation(AnnualLeave leave, int doctorId);
    }
}
