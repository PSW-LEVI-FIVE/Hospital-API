using System.Threading.Tasks;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Users.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    { 
        User GetOneByUsername(string username);
        bool UsernameExist(string username);
        bool PasswordExist(string password);
        public User UserExist(string username, string password);
    }
}