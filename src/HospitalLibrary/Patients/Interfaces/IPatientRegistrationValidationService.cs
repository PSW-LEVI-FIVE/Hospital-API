using System.Threading.Tasks;
using HospitalLibrary.Patients;

namespace HospitalLibrary.Shared.Validators
{
    public interface IPatientRegistrationValidationService
    {
        Task ValidatePatient(Patient patient);
    }
}