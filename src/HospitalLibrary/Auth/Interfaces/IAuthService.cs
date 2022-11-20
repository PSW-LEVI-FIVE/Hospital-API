using HospitalLibrary.Allergens;
using HospitalLibrary.Doctors;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Users.Dtos;

namespace HospitalLibrary.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<PatientDTO> RegisterPatient(CreatePatientDTO createPatientDto);
        
        Users.User UserExist(string username, string password);

        Task<List<Allergen>> GetPatientsAllergens(List<AllergenDTO> allergenDTOs);

        Task<Doctor> GetPatientsDoctor(string doctorUid);
        string Generate(UserDTO user);
        UserDTO Authenticate(UserDTO userDto);
    }
}