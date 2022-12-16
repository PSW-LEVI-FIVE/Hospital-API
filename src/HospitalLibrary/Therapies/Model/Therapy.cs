using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctors;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Therapies.Model
{
    public class Therapy: BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        
        public int HospitalizationId { get; private set; }
        
        [ForeignKey("HospitalizationId")]
        public Hospitalization Hospitalization { get; private set; }
        
        public string InstanceType { get; private set; }
        
        public DateTime GivenAt { get; private set; }
        
        public int DoctorId { get; private set; }
        
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; private set; }
        
    
        public Therapy(int hospitalizationId, DateTime givenAt, int doctorId)
        {
            HospitalizationId = hospitalizationId;
            GivenAt = givenAt;
            DoctorId = doctorId;
        }
    }
}