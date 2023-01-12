using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Dtos;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Examination;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Dtos;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using SendGrid.Helpers.Errors.Model;
using ceTe.DynamicPDF;
using System.Collections;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments.Dtos;

namespace HospitalLibrary.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ITimeIntervalValidationService _intervalValidation;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorService _doctorService;
        private readonly ISchedulingAppointmentStatistics _schedulingAppointmentStatistics;

        public AppointmentService(IUnitOfWork unitOfWork, ITimeIntervalValidationService intervalValidation,
            IDoctorService doctorService,ISchedulingAppointmentStatistics schedulingAppointmentStatistics)
        {
            _unitOfWork = unitOfWork;
            _intervalValidation = intervalValidation;
            _doctorService = doctorService;
            _schedulingAppointmentStatistics = schedulingAppointmentStatistics;
        }

        public Task<IEnumerable<Appointment>> GetAll()
        {
            return _unitOfWork.AppointmentRepository.GetAll();
        }

        public void Update(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.Update(appointment);
            _unitOfWork.AppointmentRepository.Save();
        }
        
        public async void Schedule(Appointment appointment)
        {
            await _intervalValidation.ValidateAppointment(appointment);
            _unitOfWork.AppointmentRepository.Update(appointment);
            _unitOfWork.AppointmentRepository.Save();
        }

        public async Task<IEnumerable<TimeInterval>> GetTimeIntervalsForStepByStep(int doctorId, DateTime chosen)
        {
            WorkingHours doctorsWorkingHours =
                _unitOfWork.WorkingHoursRepository.GetOne((int)chosen.DayOfWeek, doctorId);
            List<TimeInterval> possibleTimeIntervals = new List<TimeInterval>();
            TimeSpan timeIntervalFiller = doctorsWorkingHours.Start;
            TimeSpan timeSpanIncrementer = TimeSpan.FromMinutes(30);
            while (TimeSpan.Compare(timeIntervalFiller.Add(timeSpanIncrementer), doctorsWorkingHours.End) < 0)
            {
                TimeInterval possibleTimeInterval = new TimeInterval(chosen, timeIntervalFiller,
                    timeIntervalFiller.Add(timeSpanIncrementer));
                timeIntervalFiller = timeIntervalFiller.Add(TimeSpan.FromMinutes(35));
                bool isOverlapped =
                    await _intervalValidation.IsIntervalOverlapingWithDoctorAppointments(doctorId,
                        possibleTimeInterval);
                if (isOverlapped)
                    continue;
                possibleTimeIntervals.Add(possibleTimeInterval);
            }

            return possibleTimeIntervals;
        }

        public async Task<IEnumerable<TimeIntervalWithDoctorDTO>> GetTimeIntervalsForRecommendation(Doctor doctor,
            DateTime start, DateTime end)
        {
            List<TimeIntervalWithDoctorDTO> possibleTimeIntervals = new List<TimeIntervalWithDoctorDTO>();
            DateTime dateIterator = start;
            while (dateIterator <= end)
            {
                await GetTimeIntervalsWithDoctorForDate(doctor, dateIterator, possibleTimeIntervals);
                dateIterator = dateIterator.AddDays(1);
            }

            return possibleTimeIntervals;
        }

        public async Task<IEnumerable<TimeIntervalWithDoctorDTO>> GetTimeIntervalsForRecommendationDatePriority(
            int patientId, string speciality, DateTime start, DateTime end)
        {
            List<TimeIntervalWithDoctorDTO> possibleTimeIntervals = new List<TimeIntervalWithDoctorDTO>();
            IEnumerable<Doctor> possibleDoctors =
                await _doctorService.GetDoctorForPatientBySpeciality(patientId, speciality);
            foreach (Doctor doctor in possibleDoctors)
            {
                DateTime dateIterator = start;
                while (dateIterator <= end)
                {
                    await GetTimeIntervalsWithDoctorForDate(doctor, dateIterator, possibleTimeIntervals);
                    dateIterator = dateIterator.AddDays(1);
                }
            }

            return possibleTimeIntervals;
        }

        private async Task GetTimeIntervalsWithDoctorForDate(Doctor doctor, DateTime dateIterator,
            List<TimeIntervalWithDoctorDTO> possibleTimeIntervals)
        {
            IEnumerable<TimeInterval> timeIntervalsToAdd = await GetTimeIntervalsForStepByStep(doctor.Id, dateIterator);
            possibleTimeIntervals.AddRange(timeIntervalsToAdd.Select(timeInterval =>
                new TimeIntervalWithDoctorDTO(timeInterval,
                    new PatientsDoctorDTO(doctor.Name, doctor.Surname, doctor.Uid))));
        }

        public async Task<Appointment> Create(Appointment appointment)
        {
            await _intervalValidation.ValidateAppointment(appointment);
            _unitOfWork.AppointmentRepository.Add(appointment);
            _unitOfWork.AppointmentRepository.Save();
            return appointment;
        }
        
        public async Task<Appointment> CreateEmpty(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.Add(appointment);
            _unitOfWork.AppointmentRepository.Save();
            return appointment;
        }

        public AppointmentCancelledDTO CancelAppointment(int appointmentId)
        {
            Appointment canceled = _unitOfWork.AppointmentRepository.GetOne(appointmentId);
            canceled.State = AppointmentState.DELETED;
            Patient toNotify = _unitOfWork.PatientRepository.GetOne(canceled.PatientId ?? 0);
            AppointmentCancelledDTO retDto = new AppointmentCancelledDTO
                { PatientEmail = toNotify.Email, AppointmentTime = canceled.StartAt };
            _unitOfWork.AppointmentRepository.Update(canceled);
            _unitOfWork.AppointmentRepository.Save();
            return retDto;
        }
        
        public Appointment CancelPatientAppointment(int appointmentId)
        {
            Appointment canceled = _unitOfWork.AppointmentRepository.GetOne(appointmentId);
            Appointment retAppointment = null;
            canceled.State = AppointmentState.DELETED;
            if (!((canceled.StartAt.Date.AddDays(-1).Equals(DateTime.Now.Date) 
                   && ((canceled.StartAt.Hour < DateTime.Now.Hour) || (canceled.StartAt.Hour == DateTime.Now.Hour && canceled.StartAt.Minute < DateTime.Now.Minute)))
                  || (canceled.StartAt.Date.Equals(DateTime.Now.Date))))
            {
                _unitOfWork.AppointmentRepository.Update(canceled);
                _unitOfWork.AppointmentRepository.Save();
                retAppointment = canceled;
            }
            return retAppointment;
        }

        public Task<IEnumerable<Appointment>> GetUpcomingForDoctor(Doctor doctor)
        {
            return _unitOfWork.AppointmentRepository.GetAllDoctorUpcomingAppointments(doctor.Id);
        }


        public Task<IEnumerable<Appointment>> GetAllPatientAppointments(int patientId)
        {
            return _unitOfWork.AppointmentRepository.GetAllPatientAppointments(patientId);
        }

        public Task<IEnumerable<Appointment>> GetUpcomingAppointmentsForRoom(int roomId)
            {
                return _unitOfWork.AppointmentRepository.GetAllRoomUpcomingAppointments(roomId);

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
                return new AppointmentRescheduledDTO
                    { PatientEmail = toNotify.Email, AppointmentTimeBefore = preChange };

            }

            public Task<IEnumerable<Appointment>> GetAllForDoctorAndRange(int doctorId, TimeInterval interval)
            {
                return _unitOfWork.AppointmentRepository.GetAllDoctorAppointmentsForRange(doctorId, interval);
            }

            public IEnumerable<CalendarAppointmentsDTO> FormatAppointmentsForCalendar(
                IEnumerable<Appointment> appointments,
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
                            StartsAt = new TimeOfDayDTO(app.StartAt.TimeOfDay),
                            EndsAt = new TimeOfDayDTO(app.EndAt.TimeOfDay),
                            Patient = app.Patient != null ? app.Patient.Name + " " + app.Patient.Surname : null,
                            Type = app.Type,
                            Id = app.Id
                        });
                    map[date] = list;
                }

                return MapDictionaryToCalendarDTOs(map);
            }

            public async Task<Appointment> GetById(int appointmentId)
            {
                Appointment appointment = await _unitOfWork.AppointmentRepository.GetById(appointmentId);
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
            public List<Appointment> GetAllFinishedPatientAppointments(int patientId)
            {
                return _unitOfWork.AppointmentRepository.GetPatientEndedAppointments(patientId);
            }
            public String GetUrl()
            {
                return _unitOfWork.ExaminationReportRepository.FindExam().Url;
            }
            public ExaminationReport GetByExamination(int examinationId)
            {
                return _unitOfWork.ExaminationReportRepository.GetByExamination(examinationId);
            }
            
        public IEnumerable<AppointmentsStatisticsDTO> GetMonthStatisticsByDoctorId(int doctorId, int month)
        {
            List<AppointmentsStatisticsDTO> dailyAppointmentsDTOs = new List<AppointmentsStatisticsDTO>();
            for (int day = 1; day <= DateTime.DaysInMonth(DateTime.Now.Year, month); day++)
            {
                DateTime dayOfMonth = new DateTime(DateTime.Now.Year, month, day);
                int thisDay = _unitOfWork.AppointmentRepository.GetNumberOfDoctorAppointmentsByStartTime(doctorId, new TimeInterval(dayOfMonth, dayOfMonth.AddDays(1).AddSeconds(-1)));
                dailyAppointmentsDTOs.Add(new AppointmentsStatisticsDTO(dayOfMonth.ToString("MMM dd"), thisDay));
            }
            return dailyAppointmentsDTOs;
        }

        public IEnumerable<AppointmentsStatisticsDTO> GetYearStatisticsByDoctorId(int doctorId)
        {
            List<AppointmentsStatisticsDTO> monthlyAppointmentsDTOs = new List<AppointmentsStatisticsDTO>();
            for (int month = 1; month <= 12; month++)
            {
                DateTime firstOfMonth = new DateTime(DateTime.Now.Year, month, 1);
                int thisMonth = _unitOfWork.AppointmentRepository.GetNumberOfDoctorAppointmentsByStartTime(doctorId, new TimeInterval(firstOfMonth, firstOfMonth.AddMonths(1).AddDays(-1)));
                monthlyAppointmentsDTOs.Add(new AppointmentsStatisticsDTO(firstOfMonth.ToString("MMMM"), thisMonth));
            }
            return monthlyAppointmentsDTOs;
        }

        public IEnumerable<AppointmentsStatisticsDTO> GetTimeRangeStatisticsByDoctorId(int doctorId, TimeInterval timeInterval)
        {
            if ((timeInterval.End - timeInterval.Start).Days > 31)
                return GetMonthlyTimeRangeStatistics(doctorId,timeInterval);
            else
                return GetDailyTimeRangeStatistics(doctorId,timeInterval);
        }
        public IEnumerable<AppointmentsStatisticsDTO> GetDailyTimeRangeStatistics(int doctorId, TimeInterval timeInterval)
        {
            List<AppointmentsStatisticsDTO> timeRangeAppointmentsDTOs = new List<AppointmentsStatisticsDTO>();
            for (int day = 0; day < (timeInterval.End - timeInterval.Start).Days; day++)
            {
                DateTime dayOfMonth = timeInterval.Start.AddDays(day);
                int thisDay = _unitOfWork.AppointmentRepository.GetNumberOfDoctorAppointmentsByStartTime(doctorId, new TimeInterval(dayOfMonth, dayOfMonth.AddDays(1).AddSeconds(-1)));
                timeRangeAppointmentsDTOs.Add(new AppointmentsStatisticsDTO(dayOfMonth.ToString("MMM dd"), thisDay));
            }
            return timeRangeAppointmentsDTOs;
        }
        public IEnumerable<AppointmentsStatisticsDTO> GetMonthlyTimeRangeStatistics(int doctorId, TimeInterval timeInterval)
        {
            var result = new Dictionary<int, int>();
            for (int day = 0; day < (timeInterval.End - timeInterval.Start).Days; day++)
            {
                DateTime dayOfMonth = timeInterval.Start.AddDays(day);
                int thisDay = _unitOfWork.AppointmentRepository.GetNumberOfDoctorAppointmentsByStartTime(doctorId, new TimeInterval(dayOfMonth, dayOfMonth.AddDays(1).AddSeconds(-1)));
                if (!result.ContainsKey(dayOfMonth.Month))
                {
                    result[dayOfMonth.Month] = 0;
                }
                result[dayOfMonth.Month]+=thisDay;
            }
            return MapDictionaryToStatisticsDTOs(result);
        }
        private List<AppointmentsStatisticsDTO> MapDictionaryToStatisticsDTOs( Dictionary<int, int> result)
        {
            List<AppointmentsStatisticsDTO> timeRangeAppointmentsDTOs = new List<AppointmentsStatisticsDTO>();
            foreach (KeyValuePair<int, int> entry in result)
            {
                timeRangeAppointmentsDTOs.Add(new AppointmentsStatisticsDTO(new DateTime(DateTime.Now.Year, entry.Key, 1).ToString("MMMM"), entry.Value));
            }
            return timeRangeAppointmentsDTOs;
        }
        public void AddEvent(SchedulingAppointmentDomainEvent schedulingAppointmentDomainEvent)
        {
            var appointment = _unitOfWork.AppointmentRepository.GetOne(schedulingAppointmentDomainEvent.AggregateId);
            if (appointment == null)
            {
                throw new BadRequestException("Appointment report not found");
            }
            appointment.Apply(schedulingAppointmentDomainEvent);
            _unitOfWork.AppointmentRepository.Save();
        }
        public TimesWatchedStepsDTO CalculateStepsAverageTime()
        {
            return _schedulingAppointmentStatistics.CalculateStepsAverageTime();
        }

        public TimesWatchedStepsDTO GetTimesWatchedStep()
        {
            return _schedulingAppointmentStatistics.GetTimesWatchedStep();
        }

        public SchedulePerAgeDTO GetAverageTimeForSchedulePerAge(int fromAge, int toAge)
        {
            return _schedulingAppointmentStatistics.GetAverageTimeForSchedulePerAge(fromAge,toAge);

        }
        public SchedulePerAgeDTO GetAverageTimeForSchedule()
        {
            return _schedulingAppointmentStatistics.GetAverageTimeForSchedule();

        }
        public TimesWatchedStepsDTO GetHowManyTimesQuitOnStep()
        {
            return _schedulingAppointmentStatistics.GetHowManyTimesQuitOnStep();
        }
        public TimesWatchedStepsDTO GetLongTermedSteps()
        {
            return _schedulingAppointmentStatistics.GetLongTermedSteps();
        }
    }
}

