using System.Threading.Tasks;
using System.Collections.Generic;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using System;
using System.Linq;

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

        public User BlockMaliciousUser(int blockUserId)
        {
            User blockUser = _unitOfWork.UserRepository.GetOne(blockUserId);
            if (_unitOfWork.PatientRepository.GetMaliciousPatients(DateTime.Now.AddDays(-30)).Result.All(patient => patient.Id != blockUser.Id))
                return null;
            blockUser.Blocked = true;
            _unitOfWork.UserRepository.Update(blockUser);
            _unitOfWork.UserRepository.Save();
            return blockUser;
        }

        public User UnBlockMaliciousUser(int unblockUserId)
        {
            User unblockUser = _unitOfWork.UserRepository.GetOne(unblockUserId);
            if (unblockUser.Blocked == false) return null;
            unblockUser.Blocked = false;
            _unitOfWork.UserRepository.Update(unblockUser);
            _unitOfWork.UserRepository.Save();
            return unblockUser;
        }
    }
}