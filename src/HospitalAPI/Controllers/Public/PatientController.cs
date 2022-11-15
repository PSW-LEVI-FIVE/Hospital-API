using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{

    [Route("api/public/patients")]
    [ApiController]
    public class PatientController: ControllerBase
    {
        private IPatientService _patientService;
        
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
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
            Patient created = await _patientService.Create(createPatientDTO.MapToModel());
            return Ok(created);
        }
    }
}