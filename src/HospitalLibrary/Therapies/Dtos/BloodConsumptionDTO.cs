using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Therapies.Dtos
{
    public class BloodConsumptionDTO
    { 
        [Required]
        public int Id { get; set; }
            
        [Required]
        public int TypeBlood { get; set; }
        
        [Required]
        public double  Quantity { get; set; }
        
        [Required]
        public string DoctorName { get; set; }
        
        [Required]
        public string DoctorSurname { get; set; }
        
        [Required]
        public DateTime PrescribedDate { get; set; }
    }
}