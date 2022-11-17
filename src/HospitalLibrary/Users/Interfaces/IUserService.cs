using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Patients;

namespace HospitalLibrary.User.Interfaces
{
    public interface IUserService
    {
        Task<Users.User> Create(Users.User user);
        
        Users.User getOne(int id);
        
        Task<IEnumerable<Users.User>> GetAll();

        bool UsernameExist(string username);
        
        bool PasswordExist(string password);

        public Users.User UserExist(string username, string password);


    }
}