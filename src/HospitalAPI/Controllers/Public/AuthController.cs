using System.Threading.Tasks;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Users;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost]
        [Route("register/patient")]
        public async Task<IActionResult> RegisterPatient(CreatePatientDTO createPatientDTO)
        {
            User user = createPatientDTO.MapUserToModel();
            user.Person = createPatientDTO.MapPatientToModel();
            User createdUser = await _authService.RegisterPatient(user,createPatientDTO.Allergens);
            return Ok(createdUser);
        }
    }

}