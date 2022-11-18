using System;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctors;
using HospitalLibrary.Hospitalizations;

namespace HospitalLibrary.Therapies.Model
{
    public class Therapy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("Hospitalization")]
        public int HospitalizationId { get; set; }
        public Hospitalization Hospitalization { get; set; }
        
        public string InstanceType { get; set; }
        
        public DateTime GivenAt { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        
    
        public Therapy( int hospitalizationId, DateTime givenAt, int doctorId)
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