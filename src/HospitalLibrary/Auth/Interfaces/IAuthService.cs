using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens.Dtos;

namespace HospitalLibrary.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<Users.User> RegisterPatient(Users.User user,List<AllergenDTO> allergens,string doctorUid);
    }
}