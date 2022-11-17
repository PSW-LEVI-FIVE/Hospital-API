using System.Threading.Tasks;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users.Interfaces;

namespace HospitalLibrary.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRegistrationValidationService _registrationValidation;
        private IUnitOfWork _unitOfWork;
        
        public AuthService(IUnitOfWork unitOfWork,IRegistrationValidationService registrationValidation)
        {
            _unitOfWork = unitOfWork;
            _registrationValidation = registrationValidation;
        }
        
        public async Task<Users.User> RegisterPatient(Users.User user)
        {
            await _registrationValidation.ValidatePatientRegistration(user);
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.UserRepository.Save();
            return user;
        }
    }
}