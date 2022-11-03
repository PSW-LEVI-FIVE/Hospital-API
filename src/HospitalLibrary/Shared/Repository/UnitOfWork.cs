using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Buildings;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Floors;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Shared.Repository
{
    public class UnitOfWork: IUnitOfWork
    {

        private HospitalDbContext _dataContext;
        
        private IRoomRepository _roomRepository;
        private IFeedbackRepository _feedbackRepository;
        private IDoctorRepository _doctorRepository;
        private IPatientRepository _patientRepository;
        private IAppointmentRepository _appointmentRepository;
        private IWorkingHoursRepository _workingHoursRepository;
        private IFloorRepository _floorRepository;
        private IBuildingRepository _buildingRepository;

        public UnitOfWork(HospitalDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public IRoomRepository RoomRepository => _roomRepository ?? new RoomRepository(_dataContext);
        public IFeedbackRepository FeedbackRepository => _feedbackRepository ?? new FeedbackRepository(_dataContext);
        public IDoctorRepository DoctorRepository => _doctorRepository ?? new DoctorRepository(_dataContext);
        public IPatientRepository PatientRepository => _patientRepository ?? new PatientRepository(_dataContext);
        public IAppointmentRepository AppointmentRepository => _appointmentRepository ?? new AppointmentRepository(_dataContext);
        public IWorkingHoursRepository WorkingHoursRepository =>
            _workingHoursRepository ?? new WorkingHoursRepository(_dataContext);

        public IFloorRepository FloorRepository => _floorRepository ?? new FloorRepository(_dataContext);
        public IBuildingRepository BuildingRepository => _buildingRepository ?? new BuildingRepository(_dataContext);

    }
}