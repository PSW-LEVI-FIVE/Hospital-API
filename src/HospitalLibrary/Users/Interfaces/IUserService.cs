using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Patients;

namespace HospitalLibrary.User.Interfaces
{
    public interface IUserService
    {
        Users.User getOne(int id);
        
        Task<IEnumerable<Users.User>> GetAll();

        bool UsernameExist(string username);
        bool IsCodeUnique(string code);
        Task<Users.User> Create(Users.User user);
        Task<Users.User> ActivateAccount(Users.User user,string code);
        Task<Users.User> GetOneByCode(string code);

        public Users.User UserExist(string username, string password);
        
    }
}