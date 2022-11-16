using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{

    [Route("api/public/patients")]
    [ApiController]
    public class PatientController: ControllerBase
    {
        private IPatientService _patientService;
        private IUserService _userService;
        
        public PatientController(IPatientService patientService,IUserService userService)
        {
            _patientService = patientService;
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Patient> patients = await _patientService.GetAll();
            return Ok(patients);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientDTO createPatientDTO)
        {
            Patient createdPatient = await _patientService.Create(createPatientDTO.MapPatientToModel());
            return Ok(createdPatient);
        }
    }
}