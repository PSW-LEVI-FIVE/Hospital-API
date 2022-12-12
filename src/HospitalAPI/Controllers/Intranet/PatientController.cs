using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("maliciouspatints")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetMaliciousPatients()
        {
            List<PotentialMaliciousPatientDTO> potentialMaliciousPatients = new List<PotentialMaliciousPatientDTO>();
            foreach(Patient patient in await _patientService.GetMaliciousPatients())
            {
                potentialMaliciousPatients.Append(new PotentialMaliciousPatientDTO(patient));
            }
            return Ok(potentialMaliciousPatients);
        }


    }
}