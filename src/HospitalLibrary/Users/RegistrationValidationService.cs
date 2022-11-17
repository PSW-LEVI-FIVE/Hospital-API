using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users.Interfaces;

namespace HospitalLibrary.Users
{
    public class RegistrationValidationService:IRegistrationValidationService
    {
        private IUnitOfWork _unitOfWork;

        public RegistrationValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task ValidatePatientRegistration(User user)
        {
            if (_unitOfWork.PersonRepository.GetOneByEmail(user.Person.Email) != null)
                throw new BadRequestException("Email is already taken"); 
            if(_unitOfWork.PersonRepository.GetOneByUid(user.Person.Uid) != null)
                throw new BadRequestException("Uid is already taken");
            if(_unitOfWork.UserRepository.GetOneByUsername(user.Username) != null)
                throw new BadRequestException("Username is already taken");
        }
    }
}