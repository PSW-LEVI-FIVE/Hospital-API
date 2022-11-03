using System;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Rooms.Interfaces;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRoomRepository RoomRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IPatientRepository PatientRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IWorkingHoursRepository WorkingHoursRepository { get; }
        IFloorRepository FloorRepository { get; }
        IBuildingRepository BuildingRepository { get; }
        IMapBuildingRepository MapBuildingRepository { get; }
        IMapFloorRepository MapFloorRepository { get; }
        IMapRoomRepository MapRoomRepository { get; }
    }
}