
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HospitalDbContext context) : base(context)
        {
        }

        public User GetOneByUsername(string username)
        {
            return _dataContext.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
        }

        public bool UsernameExist(string username)
        {
            return _dataContext.Users.Any(m => m.Username.Equals(username));
        }

        public User UserExist(string username, string password)
        {
            return _dataContext.Users.FirstOrDefault(m => m.Password.Equals(password) && m.Username.Equals(username));
        }

        public bool IsCodeUnique(string code)
        {
            return (_dataContext.Users.FirstOrDefault(u => u.ActivationCode.Equals(code)) == null);
        }
        public Task<User> GetOneByCode(string code)
        {
            return _dataContext.Users
                .Where(u => u.ActivationCode.Equals(code))
                .Include(u => u.Person)
                .FirstOrDefaultAsync();
        }
    }
}