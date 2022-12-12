using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Dtos;

namespace HospitalLibrary.Appointments.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<IEnumerable<TimeInterval>> GetTimeIntervalsForStepByStep(int doctorId, DateTime chosen);
        Task<IEnumerable<TimeIntervalWithDoctorDTO>> GetTimeIntervalsForRecommendation(Doctor doctor, DateTime start, DateTime end);
        Task<IEnumerable<TimeIntervalWithDoctorDTO>> GetTimeIntervalsForRecommendationDatePriority(int patientId, string speciality, DateTime start, DateTime end);
        AppointmentCancelledDTO CancelAppointment(int appointmentId);

        Task<IEnumerable<Appointment>> GetUpcomingForDoctor(Doctor doctor);

        Task<Appointment> Create(Appointment appointment);

        public Task<IEnumerable<Appointment>> GetAllPatientAppointments(int patientId);
        
        Task<AppointmentRescheduledDTO> Reschedule(int appointmentId, DateTime start, DateTime end);

        Task<IEnumerable<Appointment>> GetAllForDoctorAndRange(int doctorId, TimeInterval interval);

        IEnumerable<CalendarAppointmentsDTO> FormatAppointmentsForCalendar(IEnumerable<Appointment> appointments, TimeInterval interval);
        Task<Appointment> GetById(int appointmentId);

    }
}