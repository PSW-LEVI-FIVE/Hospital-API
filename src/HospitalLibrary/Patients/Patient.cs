using System;
using System.Collections.Generic;
using HospitalLibrary.Allergens;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Patients
{
    public class Patient:Person
    {
        public BloodType BloodType { get; set; }
        public List<Allergen> Allergens { get; set; }
        public Patient() {}
        public Patient(string name, string surname, string email, string uid, string phoneNumber, DateTime birthDate, string address, BloodType bloodType,List<Allergen> allergens) : base( name, surname, email, uid, phoneNumber, birthDate, address)
        {
            BloodType = bloodType;
            Allergens = allergens;
        }
    }
}