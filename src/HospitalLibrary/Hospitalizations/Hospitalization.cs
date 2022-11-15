using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Therapies;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Hospitalizations
{
    
    public enum HospitalizationState { ACTIVE, FINISHED }
    public class Hospitalization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Bed")]
        public int BedId;

        public Bed bed;

        [ForeignKey("MedicalRecord")]
        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        public HospitalizationState State { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public List<Therapy> Therapies { get; set; }

        public Hospitalization(int id, int bedId, int medicalRecordId, DateTime startTime, HospitalizationState state)
        {
            BedId = bedId;
            Id = id;
            MedicalRecordId = medicalRecordId;
            StartTime = startTime;
            State = state;
        }
        
        public Hospitalization() {}
    }
}