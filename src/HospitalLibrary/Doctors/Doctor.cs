using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HospitalLibrary.Appointments;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Model.ValueObjects;


namespace HospitalLibrary.Doctors
{
    public class Doctor : Person
    {
        
        [ForeignKey("Speciality")]
        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
        public List<WorkingHours> WorkingHours { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Appointment> Appointments { get; set; }

        public Doctor(string name, string surname, string email, string uid, PhoneNumber phoneNumber, DateTime birthDate, Address address, Speciality speciality) : base(name, surname, email, uid, phoneNumber, birthDate, address)
        {
            Speciality = speciality;
        }

        public Doctor()
        {
        }

        public DateTime GetStartWorkingHoursDate(DateTime date)
        {
            TimeSpan startWork = WorkingHours.First(w => w.Day.Equals((int)date.DayOfWeek)).Start;
            return new DateTime(date.Year, date.Month, date.Day, startWork.Hours, startWork.Minutes, 0);
        }   
        public DateTime GetEndWorkingHoursDate(DateTime date)
        {
            TimeSpan endWork = WorkingHours.First(w => w.Day.Equals((int)date.DayOfWeek)).End;
            return new DateTime(date.Year, date.Month, date.Day, endWork.Hours, endWork.Minutes, 0);
        }
        
        
    }
}