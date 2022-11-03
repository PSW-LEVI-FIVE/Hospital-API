using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Appointments.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();

       AppointmentCancelledDTO CancelAppointment(int appointmentId);

        Task<IEnumerable<Appointment>> GetUpcomingForDoctor(Doctor doctor);
        
        Task<Appointment> Create(Appointment appointment);

        Task<AppointmentRescheduledDTO> Reschedule(int appointmentId, DateTime start, DateTime end);

        Task<IEnumerable<Appointment>> GetAllForDoctorAndRange(int doctorId, TimeInterval interval);
        IEnumerable<CalendarAppointmentsDTO> FormatAppointmentsForCalendar(IEnumerable<Appointment> appointments, TimeInterval interval);
    }
}