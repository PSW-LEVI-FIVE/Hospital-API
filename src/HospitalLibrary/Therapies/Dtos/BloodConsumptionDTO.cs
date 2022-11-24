using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Doctors;
using HospitalLibrary.Therapies.Model;

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

        public BloodConsumptionDTO(Therapy therapy, BloodTherapy bloodTherapy, Doctor doc)
        {
            Id = bloodTherapy.Id;
            Quantity = bloodTherapy.Quantity;
            TypeBlood = (int)bloodTherapy.BloodType;
            DoctorName = doc.Name;
            DoctorSurname = doc.Surname;
            PrescribedDate = therapy.GivenAt;
        }
    }
}