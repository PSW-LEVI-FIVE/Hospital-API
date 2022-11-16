using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.User.Interfaces;

namespace HospitalLibrary.Users
{
    public class UserService: IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}