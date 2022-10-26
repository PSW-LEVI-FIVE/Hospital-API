using System;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Patients
{
    public class Patient:Person
    {
        public Patient(string name, string surname, string email, string uid, string phoneNumber, DateTime birthDate, string address) : base( name, surname, email, uid, phoneNumber, birthDate, address)
        {
        }
    }
}