using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface ITimeIntervalValidationService
    {
        Task ValidateAppointment(Appointment appointment);

        Task ValidateReallocation(EquipmentReallocation reallocation);

        Task ValidateRescheduling(Appointment appointment, DateTime start, DateTime end);
        Task<bool> IsIntervalOverlapingWithDoctorAppointments(int doctorId, TimeInterval possibleTimeInterval);
    }

}