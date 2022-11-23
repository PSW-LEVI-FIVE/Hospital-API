using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Therapies.Dtos
{
    public class HospitalizationTherapiesDTO
    {
        [Required]
        public int Id { get; set; }
        
        public int TypeBlood { get; set; }
        
        public string MedicineName { get; set; }
        
        [Required]
        public string TherapyType { get; set; }
        
        [Required]
        public double  Quantity { get; set; }

        [Required]
        public DateTime PrescribedDate { get; set; }
    }
}