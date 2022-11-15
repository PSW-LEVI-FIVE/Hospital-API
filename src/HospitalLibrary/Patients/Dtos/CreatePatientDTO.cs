using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.BloodStorages;

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
        [Required]
        public BloodType BloodType { get; set; }
        
        public CreatePatientDTO(string name, string surname, string email, string uid, string phoneNumber, DateTime birthDate, string address, BloodType bloodType)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Uid = uid;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            Address = address;
            BloodType = bloodType;
        }
        
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
                Address = Address,
                BloodType = BloodType
            };
        } 
    }
}