using System;
using HospitalLibrary.Allergens.Interfaces;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Therapies.Interfaces;

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
        
        IAllergenRepository AllergenRepository { get; }
        IMedicineRepository MedicineRepository { get; }
        IMedicalRecordRepository MedicalRecordRepository { get; }
        IHospitalizationRepository HospitalizationRepository { get; }
        ITherapyRepository TherapyRepository { get; }
        IBloodStorageRepository BloodStorageRepository { get; }
        IRoomEquipmentRepository RoomEquipmentRepository { get; }
        
        IBedRepository BedRepository { get; }
        
        IAnnualLeaveRepository AnnualLeaveRepository { get; }
    }
}