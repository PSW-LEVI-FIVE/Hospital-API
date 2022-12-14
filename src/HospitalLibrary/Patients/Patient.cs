using System;
using System.Collections.Generic;
using HospitalLibrary.Allergens;
using HospitalLibrary.Appointments;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Patients
{
    public class Patient:Person
    {
        public BloodType BloodType { get; set; }
        public List<Allergen> Allergens { get; set; }
        public List<Appointment> Appointments { get; set; }
        public Doctor ChoosenDoctor { get; set; }
        public Patient() {}
        public Patient(string name, string surname, string email, string uid, PhoneNumber phoneNumber, DateTime birthDate, Address address, BloodType bloodType) : base( name, surname, email, uid, phoneNumber, birthDate, address)
        {
            BloodType = bloodType;
        }
    }
}