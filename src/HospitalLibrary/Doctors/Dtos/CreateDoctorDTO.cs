using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Doctors.Dtos
{
    public class CreateDoctorDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string  Surname { get; set; }
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
        [Required] 
        public int SpecialtyType { get; set; }
        
        public Doctor MapToModel()
        {
            return new Doctor
            {
                Name = Name,
                Email = Email,
                Surname = Surname,
                Uid = Uid,
                PhoneNumber = PhoneNumber,
                BirthDate = BirthDate,
                Address = Address,
                SpecialityId = SpecialtyType
            };
        }
        
    }
    
    

}