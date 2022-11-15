using System;

namespace HospitalLibrary.Hospitalizations.Dtos
{
    public class CreateHospitalizationDTO
    {

        public int BedId { get; set; }

        public int PatientId { get; set; }

        public int MedicalRecordId { get; set; }

        public DateTime StartTime { get; set; }

        public Hospitalization MapToModel()
        {
            return new Hospitalization
            {
                BedId = BedId,
                MedicalRecordId = MedicalRecordId,
                StartTime = StartTime,
                State = HospitalizationState.ACTIVE
            };

        }
    }
}