using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Shared.DTOValidators;
using Newtonsoft.Json.Serialization;

namespace HospitalLibrary.Patients.Dtos
{
    public class CreatePatientDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-z0-9_-]{3,19}$", ErrorMessage = "Name input not valid.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-z0-9_-]{3,19}$", ErrorMessage = "Surname input not valid.")]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Uid input not valid.")]
        public string Uid { get; set; }
        [Required]
        [RegularExpression(@"^[+]*[0-9-]+$", ErrorMessage = "Phone number input not valid.")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        [DateValidator(ErrorMessage ="Invalid date")]
        public DateTime BirthDate { get; set;  }
        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z0-9( )]+$", ErrorMessage = "Address input not valid.")]
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