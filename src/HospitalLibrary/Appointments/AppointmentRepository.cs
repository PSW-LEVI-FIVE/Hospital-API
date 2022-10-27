using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Appointments
{
    public class AppointmentRepository:BaseRepository<Appointment>,IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext context) : base(context)
        {
            
        }
        
    }
}