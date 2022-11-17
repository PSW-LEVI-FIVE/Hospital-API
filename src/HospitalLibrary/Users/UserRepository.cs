using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Users
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        public UserRepository(HospitalDbContext context): base(context) {}
        
        public bool UsernameExist(string username)
        {
            return _dataContext.Users.Any(m => m.Username.Equals(username));
        }

        public bool PasswordExist(string password)
        {
            return _dataContext.Users.Any(m => m.Password.Equals(password));
        }
        public User UserExist(string username,string password)
        {
            return _dataContext.Users.FirstOrDefault(m => m.Password.Equals(password) && m.Username.Equals(username));
        }
    }
}