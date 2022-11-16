using System.Threading.Tasks;

namespace HospitalLibrary.AnnualLeaves.Interfaces
{
    public interface IAnnualLeaveValidator
    {
        void Validate(AnnualLeave annualLeave);
        void Cancel_Validation(AnnualLeave leave, int doctorId);
    }
}