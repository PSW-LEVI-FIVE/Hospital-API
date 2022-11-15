using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Patients.Dtos
{
    public class CreatePatientDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Uid { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set;  }
        [Required]
        public string Address { get; set; }
        
        public Patient MapToModel()
        {
            return new Patient
            {
                Id = Id,
                Name = Name,
                Surname = Surname,
                Email = Email,
                Uid = Uid,
                PhoneNumber = PhoneNumber,
                BirthDate = BirthDate,
                Address = Address
            };
        } 
    }
}