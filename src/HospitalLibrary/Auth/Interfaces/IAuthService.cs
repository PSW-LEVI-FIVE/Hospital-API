using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Patients.Dtos;

namespace HospitalLibrary.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<Users.User> RegisterPatient(CreatePatientDTO createPatientDto);
    }
}