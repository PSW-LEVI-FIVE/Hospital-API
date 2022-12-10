using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    
    [Route("api/intranet/patients")]
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

        [HttpGet]
        [Route("search")]
        public IActionResult Search([FromQuery] string uid)
        {
            Patient patient = _patientService.SearchByUid(uid);
            return Ok(patient);
        }

    }
}