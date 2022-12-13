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

        public bool IsCodeUnique(string code)
        {
            return _unitOfWork.UserRepository.IsCodeUnique(code);
        }

        public async Task<User> Create(User user)
        {
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.UserRepository.Save();
            return user;
        }
        public async Task<User> ActivateAccount(User user,string code)
        {
            user.ActivateAccount(code);
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.UserRepository.Save();
            return user;
        }

        public async Task<User> GetOneByCode(string code)
        {
            return await _unitOfWork.UserRepository.GetOneByCode(code);
        }

        public User UserExist(string username, string password)
        {
            return _unitOfWork.UserRepository.UserExist(username, password);
        }
    }
}