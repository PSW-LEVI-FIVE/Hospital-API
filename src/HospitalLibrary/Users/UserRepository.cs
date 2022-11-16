using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Users.Interfaces;

namespace HospitalLibrary.Users
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        public UserRepository(HospitalDbContext context): base(context) {}
    }
}