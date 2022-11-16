using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IPatientService _patientService;
        
        public LoginController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> GetLogin([FromBody] PersonLoginDTO personLoginDto)
        {
            Console.WriteLine("login controller");
            IEnumerable<Patient> publishedFeedbacks = await _patientService.GetAll();
            return Ok(publishedFeedbacks);
        }
    }
}