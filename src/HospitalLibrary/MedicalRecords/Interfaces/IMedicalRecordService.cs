namespace HospitalLibrary.MedicalRecords.Interfaces
{
    public interface IMedicalRecordService
    {
        MedicalRecord Create(int patientId);
        MedicalRecord CreateOrGet(int patientId);
    }
}