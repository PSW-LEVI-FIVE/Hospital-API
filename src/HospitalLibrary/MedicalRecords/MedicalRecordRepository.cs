using System.Linq;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.MedicalRecords
{
    public class MedicalRecordRepository: BaseRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public bool Exists(int id)
        {
            return _dataContext.MedicalRecords.Any(m => m.Id == id);
        }

        public MedicalRecord GetByPatient(int patientId)
        {
            return _dataContext.MedicalRecords.FirstOrDefault(m => m.PatientId == patientId);
        }
    }
}