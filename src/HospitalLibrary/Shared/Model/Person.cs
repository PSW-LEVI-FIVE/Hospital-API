using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Shared.Model
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Surname { get; set; }
        public string Email { get; set; }
        public string Uid { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set;  }
        public string Address { get; set; }

        public Person(string name, string surname, string email, string uid, string phoneNumber, DateTime birthDate, string address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Uid = uid;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            Address = address;
        }
        
        public Person () {}
    }
    
    
    
}