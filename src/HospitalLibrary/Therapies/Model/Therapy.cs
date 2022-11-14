using System;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        public DateTime GivenAt { get; set; }
        
    
        public Therapy(int id, int hospitalizationId, DateTime givenAt)
        {
            Id = id;
            HospitalizationId = hospitalizationId;
            GivenAt = givenAt;
        }
    }
}