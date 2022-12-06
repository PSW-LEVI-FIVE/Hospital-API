using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IEmailService _emailService;
        private readonly IDoctorService _doctorService;
        
        public AppointmentController(IDoctorService doctorService,IAppointmentService appointmentService, IEmailService emailService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _emailService = emailService;
        }
        [HttpGet]
        [Route("time-intervals/step-by-step/{doctorUid}/{chosen}")]
        public async Task<IActionResult> GetTimeIntervalsForStepByStep(string doctorUid,DateTime chosen)
        {
            Doctor doctor = await _doctorService.GetDoctorByUid(doctorUid);
            IEnumerable<TimeInterval> timeIntervals = await _appointmentService.GetTimeIntervalsForStepByStep(doctor.Id,chosen);
            return Ok(timeIntervals);
        }
    }
}