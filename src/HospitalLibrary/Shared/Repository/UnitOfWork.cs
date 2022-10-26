using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Shared.Repository
{
    public class UnitOfWork: IUnitOfWork
    {

        private HospitalDbContext _dataContext;
        
        private IRoomRepository _roomRepository;
        private IFeedbackRepository _feedbackRepository;
        private IDoctorRepository _doctorRepository;
        private IPatientRepository _patientRepository;

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
    }
}