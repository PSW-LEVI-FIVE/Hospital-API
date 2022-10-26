using System;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Patients
{
    public class Patient:Person
    {
        public Patient(int id, string name, string surname, string email, string uid, string phoneNumber, DateTime birthDate, string address) : base(id, name, surname, email, uid, phoneNumber, birthDate, address)
        {
        }
    }
}