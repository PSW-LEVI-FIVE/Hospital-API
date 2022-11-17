using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Users.Interfaces;

namespace HospitalLibrary.Users
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        public UserRepository(HospitalDbContext context): base(context) {}
        public User GetOneByUsername(string username)
        {
            return _dataContext.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
        }
    }
}