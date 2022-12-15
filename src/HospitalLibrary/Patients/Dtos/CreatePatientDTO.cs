using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Shared.Dtos;
using HospitalLibrary.Shared.DTOValidators;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Model.ValueObjects;
using HospitalLibrary.Users;
using Newtonsoft.Json.Serialization;

namespace HospitalLibrary.Patients.Dtos
{
    public class CreatePatientDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Name input not valid.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Surname input not valid.")]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Uid input not valid.")]
        public string Uid { get; set; }
        [Required]
        public PhoneNumber PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        [DateValidator(ErrorMessage ="Invalid date")]
        public DateTime BirthDate { get; set;  }
        [Required]
        public AddressDTO Address { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{5}[A-Za-z0-9]+$", ErrorMessage = "Password not valid or must be longer.")]
        public string Password { get; set; }
        [Required]
        public BloodType BloodType { get; set; }
        [Required]
        public List<AllergenDTO> Allergens { get; set; }
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Doctor uid not valid.")]
        public string DoctorUid { get; set; }
        
        public CreatePatientDTO(string name, string surname, string email, string uid, 
            PhoneNumber phoneNumber, DateTime birthDate, AddressDTO address, BloodType bloodType,
            string username,string password, List<AllergenDTO> allergens,string doctorUid)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Uid = uid;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            Address = address;
            BloodType = bloodType;
            Password = password;
            Username = username;
            Allergens = allergens;
            DoctorUid = doctorUid;
        }
        
        public Patient MapPatientToModel()
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
                Address = new Address(Address),
                BloodType = BloodType
            };
        } 
        public Users.User MapUserToModel()
        {
            return new Users.User
            {
                Id = Id,
                Username = Username,
                Password = Password,
                Role = Role.Patient,
                ActiveStatus = ActiveStatus.Pending
            };
        }
    }
}