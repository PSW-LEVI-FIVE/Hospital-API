using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Invitations;
using HospitalLibrary.Renovations.Model;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface ITimeIntervalValidationService
    {
        Task ValidateAppointment(Appointment appointment);
        Task ValidateTeamBuildingEventInvitation(Invitation invitation);

        Task ValidateReallocation(EquipmentReallocation reallocation);
        void ThrowIfEndBeforeStart(DateTime start, DateTime end);
        Task ValidateRescheduling(Appointment appointment, DateTime start, DateTime end);
        Task<bool> IsIntervalOverlapingWithDoctorAppointments(int doctorId, TimeInterval possibleTimeInterval);
        Task<bool> IsTimeIntervalOverlapingWithRoomsAppointments(int roomId,TimeInterval possibleTimeInterval);
        Task ValidateRenovation(Renovations.Model.Renovation renovation);

  }

}
