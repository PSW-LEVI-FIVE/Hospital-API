using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IEmailService _emailService;

        public AppointmentController(IAppointmentService appointmentService, IEmailService emailService)
        {
            _appointmentService = appointmentService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAll();
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO createAppointmentDto)
        {
            var appointment = await _appointmentService.Create(createAppointmentDto.MapToModel());
            return Ok(appointment);
        }

        [Route("{id:int}")]
        [HttpPatch]
        public async Task<IActionResult> Reschedule(int id, [FromBody] RescheduleDTO rescheduleDto)
        {
            var appointment =
                await _appointmentService.Reschedule(id, rescheduleDto.Start, rescheduleDto.End);
            _emailService.SendAppointmentRescheduledEmail(appointment.PatientEmail, appointment.AppointmentTimeBefore,
                rescheduleDto.Start);
            return Ok(appointment);
        }

        [Route("{startDate}/week")]
        [HttpGet]
        public async Task<IActionResult> GetCalendarIntervals(DateTime startDate)
        {
            var doctorId = 2;
            var interval = new TimeInterval(startDate, startDate.AddDays(7).Date);
            var appointments =
                await _appointmentService.GetAllForDoctorAndRange(doctorId, interval);
            var calendarIntervals =
                _appointmentService.FormatAppointmentsForCalendar(appointments, interval);
            return Ok(calendarIntervals);
        }

        [Route("cancel/{id:int}")]
        [HttpPatch]
        public IActionResult Cancel(int id)
        {
            var appointment = _appointmentService.CancelAppointment(id);
            _emailService.SendAppointmentCanceledEmail(appointment.PatientEmail, appointment.AppointmentTime);
            return Ok(appointment);
        }
    }
}