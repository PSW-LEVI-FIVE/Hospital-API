using System.Threading.Tasks;

namespace HospitalLibrary.Users.Interfaces
{
    public interface IRegistrationValidationService
    {
        Task ValidatePatientRegistration(User user);
    }
}