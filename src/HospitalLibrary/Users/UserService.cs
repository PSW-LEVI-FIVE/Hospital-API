using System.Threading.Tasks;
using System.Collections.Generic;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;

namespace HospitalLibrary.Users
{
    public class UserService: IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IPatientService patientService;

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
        public async Task<User> ActivateAccount(User user)
        {
            user.ActivateAccount();
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

        public User BlockMaliciousUser(int blockUserId)
        {
            User blockUser = _unitOfWork.UserRepository.GetOne(blockUserId);
            foreach(Patient patient in patientService.GetMaliciousPatients().Result)
            {
                if(patient.Id == blockUser.Id)
                {
                    blockUser.Blocked = true;
                    _unitOfWork.UserRepository.Update(blockUser);
                    _unitOfWork.UserRepository.Save();
                    return blockUser;
                }
            }
            return null;
        }

        public User UnBlockMaliciousUser(int blockUserId)
        {
            User unblockUser = _unitOfWork.UserRepository.GetOne(blockUserId);
            if (unblockUser.Blocked == false) return null;
            unblockUser.Blocked = false;
            _unitOfWork.UserRepository.Update(unblockUser);
            _unitOfWork.UserRepository.Save();
            return unblockUser;
        }
    }
}