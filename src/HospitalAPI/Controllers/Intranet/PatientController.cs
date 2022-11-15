﻿using System;
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
        
        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            Patient created = await _patientService.Create(patient);
            return Ok(created);
        }
        
    }
}