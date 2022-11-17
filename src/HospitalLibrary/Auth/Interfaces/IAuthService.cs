using System.Threading.Tasks;

namespace HospitalLibrary.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<Users.User> RegisterPatient(Users.User user);
    }
}