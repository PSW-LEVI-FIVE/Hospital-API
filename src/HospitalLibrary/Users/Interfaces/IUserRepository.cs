using System.Threading.Tasks;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Users.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    { 
        User GetOneByUsername(string username);
        bool UsernameExist(string username);
        Task<User> GetOneByCode(string code);
        public bool IsCodeUnique(string code);
        public User GetPopulatedWithPerson(int userId);
    }
}