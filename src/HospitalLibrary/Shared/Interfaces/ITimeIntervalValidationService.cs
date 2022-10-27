using HospitalLibrary.Appointments;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface ITimeIntervalValidationService
    {
        void ValidateAppointment(Appointment appointment);
    }

}