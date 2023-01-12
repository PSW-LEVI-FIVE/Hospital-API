using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Doctors;
using HospitalLibrary.Examination;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments.Dtos;
using HospitalLibrary.Shared.Dtos;

namespace HospitalLibrary.Appointments.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();
        public void Update(Appointment appointment);
        Task<IEnumerable<TimeInterval>> GetTimeIntervalsForStepByStep(int doctorId, DateTime chosen);
        Task<IEnumerable<TimeIntervalWithDoctorDTO>> GetTimeIntervalsForRecommendation(Doctor doctor, DateTime start, DateTime end);
        Task<IEnumerable<TimeIntervalWithDoctorDTO>> GetTimeIntervalsForRecommendationDatePriority(int patientId, string speciality, DateTime start, DateTime end);
        AppointmentCancelledDTO CancelAppointment(int appointmentId);
        Appointment CancelPatientAppointment(int appointmentId);
        Task<IEnumerable<Appointment>> GetUpcomingForDoctor(Doctor doctor);
        public void Schedule(Appointment appointment);
        Task<IEnumerable<Appointment>> GetUpcomingAppointmentsForRoom(int roomId);
        Task<Appointment> Create(Appointment appointment);
        
        Task<Appointment> CreateEmpty(Appointment appointment);

        public Task<IEnumerable<Appointment>> GetAllPatientAppointments(int patientId);
        
        Task<AppointmentRescheduledDTO> Reschedule(int appointmentId, DateTime start, DateTime end);

        Task<IEnumerable<Appointment>> GetAllForDoctorAndRange(int doctorId, TimeInterval interval);

        IEnumerable<CalendarAppointmentsDTO> FormatAppointmentsForCalendar(IEnumerable<Appointment> appointments, TimeInterval interval);
        Task<Appointment> GetById(int appointmentId);
        List<Appointment> GetAllFinishedPatientAppointments(int patientId);
        String GetUrl();
        ExaminationReport GetByExamination(int examinationId);
        IEnumerable<AppointmentsStatisticsDTO> GetMonthStatisticsByDoctorId(int doctorId, int month);
        IEnumerable<AppointmentsStatisticsDTO> GetYearStatisticsByDoctorId(int doctorId);
        IEnumerable<AppointmentsStatisticsDTO> GetTimeRangeStatisticsByDoctorId(int doctorId, TimeInterval timeInterval);
        public void AddEvent(SchedulingAppointmentDomainEvent schedulingAppointmentDomainEvent);
        TimesWatchedStepsDTO CalculateStepsAverageTime();
        TimesWatchedStepsDTO GetTimesWatchedStep();
        SchedulePerAgeDTO GetAverageTimeForSchedulePerAge(int fromAge, int toAge);
        SchedulePerAgeDTO GetAverageTimeForSchedule();
        TimesWatchedStepsDTO GetLongTermedSteps();
        TimesWatchedStepsDTO GetHowManyTimesQuitOnStep();

    }
}