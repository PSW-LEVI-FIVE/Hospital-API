using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/appointments")]
    [ApiController]
    //[Authorize(Roles="Doctor, Manager")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IEmailService _emailService;

        public AppointmentController(IAppointmentService appointmentService, IEmailService emailService)
        {
            _appointmentService = appointmentService;
            _emailService = emailService;
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            Appointment appointment = await _appointmentService.GetById(id);
            return Ok(appointment);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Appointment> appointments = await _appointmentService.GetAll();
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO createAppointmentDto)
        {
            int doctorId = GetCurrentUser().Id;            
            Appointment newApp = createAppointmentDto.MapToModel();
            newApp.DoctorId = doctorId;
            Appointment appointment = await _appointmentService.Create(newApp);
            return Ok(appointment);
        }

        [Route("{id:int}")]
        [HttpPatch]
        public async Task<IActionResult> Reschedule(int id, [FromBody] RescheduleDTO rescheduleDto)
        {
            AppointmentRescheduledDTO appointment =
                await _appointmentService.Reschedule(id, rescheduleDto.Start, rescheduleDto.End);
            _emailService.SendAppointmentRescheduledEmail(appointment.PatientEmail, appointment.AppointmentTimeBefore,
                rescheduleDto.Start);
            return Ok(appointment);
        }

        [Route("{startDate}/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCalendarIntervalsForDoctorInRange(DateTime startDate, int id)
        {
            TimeInterval interval = new TimeInterval(startDate.AddDays(-3), startDate.AddDays(4));
            IEnumerable<Appointment> appointments =
                await _appointmentService.GetAllForDoctorAndRange(id, interval);
            IEnumerable<CalendarAppointmentsDTO> calendarIntervals =
                _appointmentService.FormatAppointmentsForCalendar(appointments, interval);
            return Ok(calendarIntervals);
        }
        
        [Route("{startDate}/week")]
        [HttpGet]
        public async Task<IActionResult> GetCalendarIntervals(DateTime startDate)
        {
            int doctorId = GetCurrentUser().Id;
            TimeInterval interval = new TimeInterval(startDate, startDate.AddDays(7).Date);
            IEnumerable<Appointment> appointments =
                await _appointmentService.GetAllForDoctorAndRange(doctorId, interval);
            IEnumerable<CalendarAppointmentsDTO> calendarIntervals =
                _appointmentService.FormatAppointmentsForCalendar(appointments, interval);
            return Ok(calendarIntervals);
        }

        [Route("cancel/{id:int}")]
        [HttpPatch]
        public IActionResult Cancel(int id)
        {
            AppointmentCancelledDTO appointment = _appointmentService.CancelAppointment(id);
            _emailService.SendAppointmentCanceledEmail(appointment.PatientEmail, appointment.AppointmentTime);
                return Ok(appointment);
        }

        [Route("for")]
        [HttpPost]
        public async Task<IActionResult> CreateForAnotherDoctor([FromBody] CreateAppointmentDTO createAppointmentDto)
        {
            Appointment newApp = createAppointmentDto.MapToModel();
            Appointment appointment = await _appointmentService.Create(newApp);
            return Ok(appointment);
        }

        [Route("statistics/month/{month}/{id:int}")]
        [HttpGet]
        public IActionResult GetMonthStatisticsByDoctorId(int month, int id)
        {
            IEnumerable<AppointmentsStatisticsDTO> dailyAppointmentsDTOs = _appointmentService.GetMonthStatisticsByDoctorId(id, month);
            return Ok(dailyAppointmentsDTOs);
        }

        [Route("statistics/year/{id:int}")]
        [HttpGet]
        public IActionResult GetYearStatisticsByDoctorId(int id)
        {
            IEnumerable<AppointmentsStatisticsDTO> monthlyAppointmentsDTOs = _appointmentService.GetYearStatisticsByDoctorId(id);
            return Ok(monthlyAppointmentsDTOs);
        }

        [Route("statistics/interval/{id:int}")]
        [HttpGet]
        public IActionResult GetTimeIntervalStatisticsByDoctorId([FromQuery] TimeInterval timeInterval, int id)
        {
            IEnumerable<AppointmentsStatisticsDTO> timeIntervalAppointmentsDTOs = _appointmentService.GetTimeRangeStatisticsByDoctorId(id, timeInterval);
            return Ok(timeIntervalAppointmentsDTOs);
        }


        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserDTO
                {
                    Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    Role = Role.Doctor,
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
                };
            }

            return null;
        }
        
    }
}