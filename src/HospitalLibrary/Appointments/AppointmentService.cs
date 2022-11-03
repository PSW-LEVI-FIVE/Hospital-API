using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ITimeIntervalValidationService _intervalValidation;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork, ITimeIntervalValidationService intervalValidation)
        {
            _unitOfWork = unitOfWork;
            _intervalValidation = intervalValidation;
        }

        public Task<IEnumerable<Appointment>> GetAll()
        {
            return _unitOfWork.AppointmentRepository.GetAll();
        }

        public async Task<Appointment> Create(Appointment appointment)
        {
            await _intervalValidation.ValidateAppointment(appointment);
            _unitOfWork.AppointmentRepository.Add(appointment);
            _unitOfWork.AppointmentRepository.Save();
            return appointment;
        }

        public AppointmentCancelledDTO CancelAppointment(int appointmentId)
        {
            var canceled = _unitOfWork.AppointmentRepository.GetOne(appointmentId);
            canceled.State = AppointmentState.DELETED;
            var toNotify = _unitOfWork.PatientRepository.GetOne(canceled.PatientId);
            var retDto = new AppointmentCancelledDTO
                { PatientEmail = toNotify.Email, AppointmentTime = canceled.StartAt };
            _unitOfWork.AppointmentRepository.Update(canceled);
            _unitOfWork.AppointmentRepository.Save();
            return retDto;
        }

        public Task<IEnumerable<Appointment>> GetUpcomingForDoctor(Doctor doctor)
        {
            return _unitOfWork.AppointmentRepository.GetAllDoctorUpcomingAppointments(doctor.Id);
        }

        public async Task<AppointmentRescheduledDTO> Reschedule(int appointmentId, DateTime start, DateTime end)
        {
            var appointment = _unitOfWork.AppointmentRepository.GetOne(appointmentId);
            var preChange = appointment.StartAt;
            await _intervalValidation.ValidateRescheduling(appointment, start, end);
            appointment.StartAt = start;
            appointment.EndAt = end;
            _unitOfWork.AppointmentRepository.Update(appointment);
            _unitOfWork.AppointmentRepository.Save();
            var toNotify = _unitOfWork.PatientRepository.GetOne(appointment.PatientId);
            return new AppointmentRescheduledDTO { PatientEmail = toNotify.Email, AppointmentTimeBefore = preChange };
        }

        public Task<IEnumerable<Appointment>> GetAllForDoctorAndRange(int doctorId, TimeInterval interval)
        {
            return _unitOfWork.AppointmentRepository.GetAllDoctorAppointmentsForRange(doctorId, interval);
        }

        public IEnumerable<CalendarAppointmentsDTO> FormatAppointmentsForCalendar(IEnumerable<Appointment> appointments,
            TimeInterval interval)
        {
            var map = FillDictionaryWithStartDates(interval);
            foreach (var app in appointments)
            {
                var date = app.StartAt.Date;
                var list = map[date];
                list.Add(
                    new CalendarInterval
                    {
                        StartsAt = app.StartAt.TimeOfDay,
                        EndsAt = app.EndAt.TimeOfDay,
                        Patient = app.Patient.Name = app.Patient.Surname,
                        Id = app.Id
                    });
                map[date] = list;
            }

            return MapDictionaryToCalendarDTOs(map);
        }

        private Dictionary<DateTime, List<CalendarInterval>> FillDictionaryWithStartDates(TimeInterval interval)
        {
            var map = new Dictionary<DateTime, List<CalendarInterval>>();
            var startDate = interval.Start.Date;
            while (startDate.CompareTo(interval.End.Date) < 0)
            {
                map.Add(startDate, new List<CalendarInterval>());
                startDate = startDate.AddDays(1).Date;
            }

            return map;
        }

        private IEnumerable<CalendarAppointmentsDTO> MapDictionaryToCalendarDTOs(
            Dictionary<DateTime, List<CalendarInterval>> map)
        {
            return (from dt in map.Keys
                    let date = dt.Date.ToString("yyyy-MM-dd")
                    select new CalendarAppointmentsDTO(date, map[dt]))
                .ToList();
        }
    }
}