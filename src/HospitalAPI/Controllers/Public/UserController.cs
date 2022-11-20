using System;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/login")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IConfiguration _config;
        
        public UserController(IUserService userService,IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult UserExist([FromBody] UserDTO userDto)
        {
            var user = Authenticate(userDto);
            if (user != null)
            {
                var token = Generate(user);
                UserDTO userDTO = GetCurrentUser();
                Console.WriteLine("CurrentUser: " + userDTO.Username);
                return Ok(token);
            }

            return NotFound("User not found");
            /*User user = _userService.UserExist(userDto.Username,userDto.Password);
            if(user != null)
                Console.WriteLine(user.Username + " " + user.Password);
            else
                Console.WriteLine("User je null");
            return Ok(user);*/
        }

        [HttpGet("Doctors")]
        [Authorize]
        public IActionResult PatientsEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi {currentUser.Username}");
        }

        private string Generate(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Password),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
                claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDTO Authenticate(UserDTO userDto)
        {
            var currentUser = _userService.UserExist(userDto.Username, userDto.Password);
            if (currentUser != null)
            {
                return new UserDTO(currentUser.Username,currentUser.Password,currentUser.Role);
            }

            return null;

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