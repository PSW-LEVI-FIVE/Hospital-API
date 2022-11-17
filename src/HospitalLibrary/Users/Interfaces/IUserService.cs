using System.Threading.Tasks;

namespace HospitalLibrary.User.Interfaces
{
    public interface IUserService
    {
        Task<Users.User> Create(Users.User user);
        Users.User GetOneByUsername(string username);
    }
}