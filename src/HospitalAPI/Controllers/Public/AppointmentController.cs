using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Examination;
using HospitalLibrary.Examination.Dtos;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Dtos;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/appointments")]
    [ApiController]
    [Authorize(Roles="Patient")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IRoomService _roomService;

        public AppointmentController(IDoctorService doctorService,IRoomService roomService,IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _roomService = roomService;
        }
        [HttpGet]
        [Route("time-intervals/step-by-step/{doctorUid}/{chosen}")]
        public async Task<IActionResult> GetTimeIntervalsForStepByStep(string doctorUid,DateTime chosen)
        {
            Doctor doctor = await _doctorService.GetDoctorByUid(doctorUid);
            IEnumerable<TimeInterval> timeIntervals = await _appointmentService.GetTimeIntervalsForStepByStep(doctor.Id,chosen);
            return Ok(timeIntervals);
        }
        [HttpGet]
        [Route("time-intervals/recommended/{doctorUid}/{start}/{end}")]
        public async Task<IActionResult> GetTimeIntervalsForRecomendation(string doctorUid,DateTime start,DateTime end)
        {
            Doctor doctor = await _doctorService.GetDoctorByUid(doctorUid);
            IEnumerable<TimeIntervalWithDoctorDTO> timeIntervals = await _appointmentService.GetTimeIntervalsForRecommendation(doctor,start,end);
            return Ok(timeIntervals);
        }
        
        [HttpGet]
        [Route("time-intervals/recommended/date-priority/{speciality}/{start}/{end}")]
        public async Task<IActionResult> GetTimeIntervalsForRecommendationDatePriority(string speciality,DateTime start,DateTime end)
        {
            int patientId = GetCurrentUser().Id;
            IEnumerable<TimeIntervalWithDoctorDTO> timeIntervals = await _appointmentService.GetTimeIntervalsForRecommendationDatePriority(patientId,speciality,start,end);
            return Ok(timeIntervals);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentForPatientDTO createAppointmentDto)
        {
            int patientId = GetCurrentUser().Id;
            Appointment newApp = createAppointmentDto.MapToModel();
            newApp.RoomId = (await _roomService.GetFirstAvailableExaminationRoom(createAppointmentDto.ChosenTimeInterval)).Id;
            newApp.DoctorId = (await _doctorService.GetDoctorByUid(createAppointmentDto.DoctorUid)).Id;
            newApp.PatientId = patientId;
            Appointment appointment = await _appointmentService.Create(newApp);
            return Ok(newApp);
        }

        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null) return null;
            var userClaims = identity.Claims;
            return new UserDTO
            {
                Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                Role = Role.Patient,
                Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
            };
        }
        [HttpGet]
        [Route("myAppointments")]
        public async Task<IActionResult> GetPatientAppointments()
        {
            int patientId =  GetCurrentUser().Id;
            IEnumerable<Appointment> appointmentList = await _appointmentService.GetAllPatientAppointments(patientId);
            return Ok(appointmentList);
        }
        
        [Route("cancel")]
        [HttpPost]
        public async Task<IActionResult> Cancel([FromBody]int id)
        {
            Appointment appointment = _appointmentService.CancelPatientAppointment(id);
            if (appointment == null) return BadRequest("You can't cancel appointment 24h before start");
            return Ok(appointment);
        }
        
        [Route("finished")]
        [HttpGet]
        public async Task<IActionResult> GetAllFinishedPatientAppointments()
        {
            int patientId = GetCurrentUser().Id;
            IEnumerable<Appointment> appointments = await _appointmentService.GetAllFinishedPatientAppointments(patientId);
            return Ok(appointments);
        }
        
        [Route("pdf")]
        [HttpPost]
        public async Task<IActionResult> GetPdf([FromBody] int appointmentId)
        {
            ExaminationReport exam = _appointmentService.GetByExamination(appointmentId);
            ExaminationPdfDto examDto = new ExaminationPdfDto(exam.ExaminationId, exam.Url);
            return Ok(examDto);
        }
        
    }
}