using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Medicines;
using HospitalLibrary.Therapies.Model;

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

        public HospitalizationTherapiesDTO(Therapy therapy, BloodTherapy bTherapy)
        {
            Id = therapy.Id;
            TherapyType = therapy.InstanceType;
            Quantity = bTherapy.Quantity;
            PrescribedDate = therapy.GivenAt;
            TypeBlood =(int)bTherapy.BloodType;
        }
        
        public HospitalizationTherapiesDTO(Therapy therapy, MedicineTherapy mTherapy, Medicine med)
        {
            Id = therapy.Id;
            TherapyType = therapy.InstanceType;
            Quantity = mTherapy.Quantity;
            PrescribedDate = therapy.GivenAt;
            MedicineName = med.Name.NameString;
        }
    }
}