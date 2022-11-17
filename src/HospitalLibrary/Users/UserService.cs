using System.Threading.Tasks;
using System.Collections.Generic;
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

        public User getOne(int id)
        {
            return _unitOfWork.UserRepository.GetOne(id);
        }
        public Task<IEnumerable<User>> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public bool UsernameExist(string username)
        {
            return _unitOfWork.UserRepository.UsernameExist(username);
        }

        public bool PasswordExist(string password)
        {
            return _unitOfWork.UserRepository.PasswordExist(password);
        }

        public User UserExist(string username, string password)
        {
            return _unitOfWork.UserRepository.UserExist(username, password);
        }
    }
}