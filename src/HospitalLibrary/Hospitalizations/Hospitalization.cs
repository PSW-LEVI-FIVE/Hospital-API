using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Therapies;

namespace HospitalLibrary.Hospitalizations
{
    public class Hospitalization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Bed")]
        public int BedId;

        [ForeignKey("MedicalRecord")]
        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public List<Therapy> Therapies { get; set; }

        public Hospitalization(int bedId, int id, int medicalRecordId, DateTime startTime)
        {
            BedId = bedId;
            Id = id;
            MedicalRecordId = medicalRecordId;
            StartTime = startTime;
        }
        
        public Hospitalization() {}
    }
}