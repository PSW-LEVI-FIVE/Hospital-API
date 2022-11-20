using System.Collections.Generic;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.MedicalRecords
{
    public class MedicalRecordService: IMedicalRecordService
    {
        private IUnitOfWork _unitOfWork;

        public MedicalRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public MedicalRecord Create(int patientId)
        {
            MedicalRecord medicalRecord = new MedicalRecord { PatientId = patientId};
            _unitOfWork.MedicalRecordRepository.Add(medicalRecord);
            _unitOfWork.MedicalRecordRepository.Save();
            return medicalRecord;
        }

        public MedicalRecord CreateOrGet(int patientId)
        {
            MedicalRecord medRec = _unitOfWork.MedicalRecordRepository.GetByPatient(patientId);
            return medRec ?? Create(patientId);
        }

        public MedicalRecord GetByPatient(int id)
        {
            return _unitOfWork.MedicalRecordRepository.GetByPatientPopulated(id);
        }
    }
}