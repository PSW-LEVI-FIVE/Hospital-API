using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface ITimeIntervalValidationService
    {
        Task ValidateAppointment(Appointment appointment);

        Task ValidateRescheduling(Appointment appointment, DateTime start, DateTime end);
    }

}