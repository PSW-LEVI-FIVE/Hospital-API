using System;
using System.Collections.Generic;
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
            IEnumerable<Appointment> appointments = await _appointmentService.GetAll();
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO createAppointmentDto)
        {
            Appointment appointment = await _appointmentService.Create(createAppointmentDto.MapToModel());
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

        [Route("{startDate}/week")]
        [HttpGet]
        public async Task<IActionResult> GetCalendarIntervals(DateTime startDate)
        {
            int doctorId = 2;
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
    }
}