using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Dtos;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/doctors")]
    [ApiController]
    public class DoctorController: ControllerBase
    {
        private IDoctorService _doctorService;
        private IEmailService _emailService;

        public DoctorController(IDoctorService doctorService, IEmailService emailService)
        {
            _doctorService = doctorService;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("internal-medicine/registration")]
        public async Task<IActionResult> GetIternalMedicineDoctorsForPatientRegistration()
        {
            IEnumerable<Doctor> doctors = await _doctorService.GetIternalMedicineDoctorsForPatientRegistration();
            List <PatientsDoctorDTO> doctorDTOs = new List<PatientsDoctorDTO>();
            foreach (Doctor doctor in doctors)
            {
                PatientsDoctorDTO doctorDTO = new PatientsDoctorDTO(doctor.Name, doctor.Surname,doctor.Uid);
                doctorDTOs.Add(doctorDTO);
            }
            return Ok(doctorDTOs);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDTO doctorDto)
        {
            Doctor created = _doctorService.Create(doctorDto.MapToModel());
            _emailService.SendWelcomeEmail(created.Email);
            return Ok(created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Doctor> doctors = await _doctorService.GetAll();
            return Ok(doctors);
        }

        [HttpGet]
        [Route("statistics/mostPopularDoc")]
        public async Task<IActionResult> GetMostPopularDoctor([FromQuery(Name = "minAge")] int minAge = 0, [FromQuery(Name = "maxAge")] int maxAge = 666, [FromQuery(Name = "onlyMostPopularDoctors")] bool onlyMostPopularDoctors = true)
        {
            IEnumerable<DoctorWithPopularityDTO> mostPopularDoctors = _doctorService.GetMostPopularDoctorByAgeRange(minAge, maxAge, onlyMostPopularDoctors);
            return Ok(mostPopularDoctors);
        }
    }
}