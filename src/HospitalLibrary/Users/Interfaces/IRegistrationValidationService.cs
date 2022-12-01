using System.Threading.Tasks;

namespace HospitalLibrary.Users.Interfaces
{
    public interface IRegistrationValidationService
    {
        Task ValidatePatientRegistration(User user);
        Task<bool> IsUsernameUnique(string username);
        Task<bool> IsEmailUnique(string email);
        Task<bool> IsUidUnique(string uid);
    }
}