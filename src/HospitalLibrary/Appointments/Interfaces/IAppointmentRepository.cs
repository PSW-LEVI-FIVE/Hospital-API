using HospitalLibrary.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Appointments.Interfaces
{
    public interface IAppointmentRepository: IBaseRepository<Appointment>
    { 
        Task<IEnumerable<Appointment>> GetAllDoctorUpcomingAppointments(int doctorId);
    }
}