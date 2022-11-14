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
    }
}