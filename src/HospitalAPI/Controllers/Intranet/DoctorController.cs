using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController: ControllerBase
    {
        private IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        [HttpPost]
        public IActionResult Create()
        {
            Doctor doctor = new Doctor(1, "Srdjan", "Stjepanovic", "stjepanovic@gmail.com", "123123123", "066603434", DateTime.Now.Date, "Neka ulica", SpecialtyType.SURGERY);
            Doctor created = _doctorService.Create(doctor);
            return Ok(created);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Doctor> doctors = await _doctorService.GetAll();
            return Ok(doctors);
        }
    }
}