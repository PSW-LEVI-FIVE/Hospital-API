using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctors;
using HospitalLibrary.Hospitalizations;

namespace HospitalLibrary.Therapies.Model
{
    public class Therapy
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int HospitalizationId { get; set; }
        
        [ForeignKey("HospitalizationId")]
        public Hospitalization Hospitalization { get; set; }
        
        public string InstanceType { get; set; }
        
        public DateTime GivenAt { get; set; }
        
        public int DoctorId { get; set; }
        
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        
    
        public Therapy(int hospitalizationId, DateTime givenAt, int doctorId)
        {
            HospitalizationId = hospitalizationId;
            GivenAt = givenAt;
            DoctorId = doctorId;
        }

        public Therapy()
        {
        }
    }
}