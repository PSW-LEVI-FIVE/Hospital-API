using System.Threading.Tasks;
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

        public async Task<User> Create(User user)
        {
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.UserRepository.Save();
            return user;
        }
    }
}