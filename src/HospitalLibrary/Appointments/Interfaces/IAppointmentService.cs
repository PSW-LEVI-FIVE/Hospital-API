using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Doctors;

namespace HospitalLibrary.Appointments.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<IEnumerable<TimeInterval>> GetTimeIntervalsForStepByStep(int doctorId, DateTime chosen);

        AppointmentCancelledDTO CancelAppointment(int appointmentId);

        Task<IEnumerable<Appointment>> GetUpcomingForDoctor(Doctor doctor);

        Task<Appointment> Create(Appointment appointment);

        Task<AppointmentRescheduledDTO> Reschedule(int appointmentId, DateTime start, DateTime end);

        Task<IEnumerable<Appointment>> GetAllForDoctorAndRange(int doctorId, TimeInterval interval);

        IEnumerable<CalendarAppointmentsDTO> FormatAppointmentsForCalendar(IEnumerable<Appointment> appointments, TimeInterval interval);
        Task<Appointment> GetById(int appointmentId);

    }
}