using System.Linq;
using System.Security.Claims;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/login")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetbyId(int id)
        {
            User user = _userService.getOne(id);
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult UserExist([FromBody] UserDTO userDto)
        {
            var user = _authService.Authenticate(userDto);
            if (user != null)
            {
                var token = _authService.Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult PatientsEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi {currentUser.Username}");
        }

        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserDTO
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = Role.Doctor
                };
            }

            return null;
        }
    }
}