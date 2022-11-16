using System;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Patients
{
    public class Patient:Person
    {
        public BloodType BloodType { get; set; }
        public Patient() {}
        public Patient(string name, string surname, string email, string uid, string phoneNumber, DateTime birthDate, string address, BloodType bloodType) : base( name, surname, email, uid, phoneNumber, birthDate, address)
        {
            BloodType = bloodType;
        }
    }
}