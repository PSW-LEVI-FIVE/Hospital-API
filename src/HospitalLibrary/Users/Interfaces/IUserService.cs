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
        Task<Users.User> Create(Users.User user);

        public Users.User UserExist(string username, string password);
        
    }
}