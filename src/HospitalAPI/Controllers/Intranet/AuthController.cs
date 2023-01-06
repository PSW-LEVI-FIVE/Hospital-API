using System.Linq;
using System.Security.Claims;
using HospitalLibrary.Auth.Dtos;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private IAuthService _authService;
        private IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
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
                Role = Role.Doctor,
                Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
            };
        }
    }
}