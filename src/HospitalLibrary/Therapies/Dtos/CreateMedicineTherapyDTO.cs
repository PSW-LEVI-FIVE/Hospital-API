using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Therapies.Dtos
{
    public class CreateMedicineTherapyDTO
    {
        [Required]
        public int  HospitalizationId { get; set; }
        [Required]        
        public DateTime GivenAt { get; set;  }
        [Required]
        public int MedicineId { get; set; }
        [Required]
        public double  Quantity { get; set; }
        [Required]
        public int DoctorId { get; set; }


        public MedicineTherapy MapToModel()
        {
            return new MedicineTherapy(HospitalizationId, GivenAt, MedicineId, Quantity, DoctorId);
        }
    }
}