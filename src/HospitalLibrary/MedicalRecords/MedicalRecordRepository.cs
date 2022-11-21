using System.Linq;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

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

        public MedicalRecord GetByPatientPopulated(int patientId)
        {
            return _dataContext.MedicalRecords
                .Where(m => m.PatientId == patientId)
                .Include(m => m.Patient)
                .FirstOrDefault();
        }

        public MedicalRecord GetByPatientUidPopulated(string patientUid)
        {
            return _dataContext.MedicalRecords
                .Include(m => m.Patient)
                .FirstOrDefault(m => m.Patient.Uid.Equals(patientUid));
        }
    }
}