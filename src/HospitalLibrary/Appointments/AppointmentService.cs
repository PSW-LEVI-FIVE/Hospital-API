using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model;
using SendGrid.Helpers.Errors.Model;

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

        public async Task<IEnumerable<TimeInterval>> GetTimeIntervalsForStepByStep(int doctorId, DateTime chosen)
        {
            WorkingHours doctorsWorkingHours = _unitOfWork.WorkingHoursRepository.GetOne((int)chosen.DayOfWeek,doctorId);
            List<TimeInterval> possibleTimeIntervals = new List<TimeInterval>();
            TimeSpan timeIntervalFiller = doctorsWorkingHours.Start;
            TimeSpan timeSpanIncrementer = TimeSpan.FromMinutes(30);
            while (TimeSpan.Compare(timeIntervalFiller.Add(timeSpanIncrementer), doctorsWorkingHours.End) < 0)
            {
                TimeInterval possibleTimeInterval = new TimeInterval(chosen, timeIntervalFiller,
                    timeIntervalFiller.Add(timeSpanIncrementer));
                timeIntervalFiller = timeIntervalFiller.Add(TimeSpan.FromMinutes(35));
                bool isOverlapped = await _intervalValidation.IsIntervalOverlapingWithDoctorAppointments(doctorId, possibleTimeInterval);
                if (isOverlapped)
                    continue;
                possibleTimeIntervals.Add(possibleTimeInterval);
            }

            return possibleTimeIntervals;
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
            Appointment canceled = _unitOfWork.AppointmentRepository.GetOne(appointmentId);
            canceled.State = AppointmentState.DELETED;
            Patient toNotify = _unitOfWork.PatientRepository.GetOne(canceled.PatientId ?? 0);
            AppointmentCancelledDTO retDto = new AppointmentCancelledDTO { PatientEmail = toNotify.Email, AppointmentTime = canceled.StartAt };
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
            Appointment appointment = _unitOfWork.AppointmentRepository.GetOne(appointmentId);
            DateTime preChange = appointment.StartAt;
            await _intervalValidation.ValidateRescheduling(appointment, start, end);
            appointment.StartAt = start;
            appointment.EndAt = end;
            _unitOfWork.AppointmentRepository.Update(appointment);
            _unitOfWork.AppointmentRepository.Save();
            Patient toNotify = _unitOfWork.PatientRepository.GetOne(appointment.PatientId ?? 0);
            return new AppointmentRescheduledDTO { PatientEmail = toNotify.Email, AppointmentTimeBefore = preChange };
        }

        public Task<IEnumerable<Appointment>> GetAllForDoctorAndRange(int doctorId, TimeInterval interval)
        {
            return _unitOfWork.AppointmentRepository.GetAllDoctorAppointmentsForRange(doctorId, interval);
        }

        public IEnumerable<CalendarAppointmentsDTO> FormatAppointmentsForCalendar(IEnumerable<Appointment> appointments,
            TimeInterval interval)
        {
            Dictionary<DateTime, List<CalendarInterval>> map = FillDictionaryWithStartDates(interval);
            foreach (Appointment app in appointments)
            {
                DateTime date = app.StartAt.Date;
                List<CalendarInterval> list = map[date];
                list.Add(
                    new CalendarInterval
                    {
                        StartsAt = new TimeOfDayDTO(app.StartAt.TimeOfDay) ,
                        EndsAt = new TimeOfDayDTO(app.EndAt.TimeOfDay),
                        Patient = app.Patient == null ? app.Patient.Name  + " " + app.Patient.Surname: null,
                        Type = app.Type,
                        Id = app.Id
                    });
                map[date] = list;
            }

            return MapDictionaryToCalendarDTOs(map);
        }

        public async Task<Appointment> GetById(int appointmentId)
        {
            Appointment appointment= await _unitOfWork.AppointmentRepository.GetById(appointmentId);
            if (appointment == null)
            {
                throw new BadRequestException("Appointment with ID " + appointmentId + "does not exist");
            }
            return appointment;
        }

        private Dictionary<DateTime, List<CalendarInterval>> FillDictionaryWithStartDates(TimeInterval interval)
        {
            Dictionary<DateTime, List<CalendarInterval>> map = new Dictionary<DateTime, List<CalendarInterval>>();
            DateTime startDate = interval.Start.Date;
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