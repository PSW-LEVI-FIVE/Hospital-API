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

        Appointment Create(Appointment appointment);

        AppointmentCancelledDTO CancelAppointment(int appointmentId);

        Task<IEnumerable<Appointment>> GetUpcomingForDoctor(Doctor doctor);
    }
}