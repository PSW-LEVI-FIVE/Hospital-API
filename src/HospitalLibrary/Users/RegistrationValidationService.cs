using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users.Interfaces;

namespace HospitalLibrary.Users
{
    public class RegistrationValidationService : IRegistrationValidationService
    {
        private IUnitOfWork _unitOfWork;

        public RegistrationValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task ValidatePatientRegistration(User user)
        {
            await IsUsernameUnique(user.Username);
            await IsEmailUnique(user.Person.Email);
            await IsUidUnique(user.Person.Uid);
        }

        public async Task<bool> IsUsernameUnique(string username)
        {
            if(_unitOfWork.UserRepository.GetOneByUsername(username) != null)
                throw new BadRequestException("Username is already taken");
            return true;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            if(_unitOfWork.PersonRepository.GetOneByEmail(email) != null)
                throw new BadRequestException("Email is already taken");
            return true;
        }

        public async Task<bool> IsUidUnique(string uid)
        {
            if(_unitOfWork.PersonRepository.GetOneByUid(uid) != null)
                throw new BadRequestException("Uid is already taken");
            return true;
        }
    }
}