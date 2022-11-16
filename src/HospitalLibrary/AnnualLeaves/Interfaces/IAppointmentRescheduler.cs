using System.Threading.Tasks;
using HospitalLibrary.Appointments;

namespace HospitalLibrary.AnnualLeaves.Interfaces
{
    public interface IAppointmentRescheduler
    {
        Task Reschedule(int doctorId, TimeInterval timeInterval);
    }
}