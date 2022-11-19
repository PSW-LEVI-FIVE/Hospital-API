using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Patients;
using HospitalLibrary.Hospitalizations;

namespace HospitalLibrary.MedicalRecords
{
    public class MedicalRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        
        public List<Hospitalization> Hospitalizations { get; set; }


        public MedicalRecord(int id, int patientId, Patient patient)
        {
            Id = id;
            PatientId = patientId;
            Patient = patient;
        }
        
        public MedicalRecord() {}
    }
}