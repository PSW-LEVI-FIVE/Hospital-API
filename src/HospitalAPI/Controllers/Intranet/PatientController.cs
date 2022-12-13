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
using Microsoft.AspNetCore.Mvc.TagHelpers;

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
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetMaliciousPatients()
        {
            DateTime dateForMaliciousPatients = DateTime.Now.AddDays(-30);
            List<PotentialMaliciousPatientDTO> potentialMaliciousPatients = new List<PotentialMaliciousPatientDTO>();
            List<Patient> patients = new List<Patient>();
            foreach (Patient patient in _patientService.GetBlockedPatients(dateForMaliciousPatients))
                potentialMaliciousPatients.Add(new PotentialMaliciousPatientDTO(patient,true));
            foreach (Patient patient in _patientService.GetMaliciousPatients(dateForMaliciousPatients))
            {
                Boolean cont = false;
                foreach (PotentialMaliciousPatientDTO potentialBlockedPatient in potentialMaliciousPatients)
                    if (potentialBlockedPatient.Id == patient.Id)
                    {
                        cont = true;
                        break;
                    } 
                if(!cont) potentialMaliciousPatients.Add(new PotentialMaliciousPatientDTO(patient));
            }
            return Ok(potentialMaliciousPatients);
        }


    }
}