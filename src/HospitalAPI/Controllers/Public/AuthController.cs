﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService,IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }
        
        [HttpPost]
        [Route("register/patient")]
        public async Task<IActionResult> RegisterPatient(CreatePatientDTO createPatientDTO)
        {
            PatientDTO createdPatient = await _authService.RegisterPatient(createPatientDTO);
            await _emailService.SendWelcomeEmailWithActivationLink(createdPatient.Email);
            return Ok(createdPatient);
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