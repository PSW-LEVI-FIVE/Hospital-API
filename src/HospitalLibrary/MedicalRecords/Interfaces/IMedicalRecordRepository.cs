﻿using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.MedicalRecords.Interfaces
{
    public interface IMedicalRecordRepository: IBaseRepository<MedicalRecord>
    {
        bool Exists(int id);
        MedicalRecord GetByPatient(int patientId);

    }
}