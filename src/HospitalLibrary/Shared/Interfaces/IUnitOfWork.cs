using System;
using HospitalLibrary.Advertisement.Interfaces;
using HospitalLibrary.Allergens.Interfaces;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.BloodOrders.Interfaces;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Consiliums.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments;
using HospitalLibrary.Invitations.Interfaces;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Persons.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Symptoms.Interfaces;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Users.Interfaces;
using HospitalLibrary.Renovations.Interface;


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
        IUserRepository UserRepository { get; }
        IEquipmentReallocationRepository EquipmentReallocationRepository { get; }
        IBedRepository BedRepository { get; }
        IAnnualLeaveRepository AnnualLeaveRepository { get; }
        IBloodOrderRepository BloodOrderRepository { get; }
        IPersonRepository PersonRepository { get; }
        
        IExaminationReportRepository ExaminationReportRepository { get;  }
        ISymptomRepository SymptomRepository { get; }
        IConsiliumRepository ConsiliumRepository { get; }
        IExaminationEventRepository ExaminationEventRepository { get; }
        
        ISpecialtyRepository SpecialtyRepository { get; }
        
        IAdvertisementRepository AdvertisementRepository { get; }
        IRenovationRepository RenovationRepository { get; }
        IInvitationRepository InvitationRepository { get; }
        ISchedulingEventRepository SchedulingEvenetRepository { get; }
        
        IRenovationStaticsRepository RenovationStaticsRepository { get; }

  }
}


       
    

