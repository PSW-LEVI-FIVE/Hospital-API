using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Auth.Dtos;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService,IEmailService emailService,IUserService userService)
        {
            _authService = authService;
            _emailService = emailService;
            _userService = userService;
        }
        
        [HttpPost]
        [Route("register/patient")]
        public async Task<IActionResult> RegisterPatient(CreatePatientDTO createPatientDTO)
        {
            PatientDTO createdPatient = await _authService.RegisterPatient(createPatientDTO);
            await _emailService.SendWelcomeEmailWithActivationLink(createdPatient.Email,createdPatient.ActivationCode);
            return Ok(createdPatient);
        }
        [HttpPatch]
        [Route("activate/{code}")]
        public async Task<IActionResult> ActivateAccount(string code)
        {
            PatientDTO activatedPatient = await _authService.ActivateAccount(code);
            return Ok(activatedPatient);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult UserExist([FromBody] UserDTO userDto)
        {
            var user = _authService.Authenticate(userDto);
            if (user == null) return NotFound("User not found");
            var token = _authService.Generate(user);
            var loggedIn = new LoggedIn()
            {
                AccessToken = token,
                Role = user.Role
            };
            return Ok(loggedIn);
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult PatientsEndpoint()
        {
            var currentUser = GetCurrentUser();
            var userData = _userService.GetPopulatedWithPerson(currentUser.Id);
            var user = new Authenticated()
            {
                Username = currentUser.Username,
                Role = currentUser.Role,
                Name = userData.Person.Name,
                Surname = userData.Person.Surname
            };
            return Ok(user);
        }

        private UserDTO GetCurrentUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity) return null;
            var userClaims = identity.Claims;
            return new UserDTO
            {
                Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                Role = Role.Patient,
                Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
            };
        }
    }

}