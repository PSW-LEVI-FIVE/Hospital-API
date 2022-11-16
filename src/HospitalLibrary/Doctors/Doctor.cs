using System;
using System.Collections.Generic;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Model;

public enum SpecialtyType
{
    ALLERGY,
    ANEESTHESIOLOGY,
    DERMATOLOGY,
    FAMILY_MEDICINE,
    NEUROLOGY,
    PEDIATRICS,
    UROLOGY,
    SURGERY,
    PSYCHIATRY,
    ITERNAL_MEDICINE
}


namespace HospitalLibrary.Doctors
{
    public class Doctor : Person
    {
        public SpecialtyType SpecialtyType { get; set; }
        
        public List<WorkingHours> WorkingHours { get; set; }

        public Doctor(string name, string surname, string email, string uid, string phoneNumber, DateTime birthDate, string address, SpecialtyType specialtyType) : base(name, surname, email, uid, phoneNumber, birthDate, address)
        {
            SpecialtyType = specialtyType;
        }

        public Doctor()
        {
        }

        public bool DoesAppointmentFollowWorkingHours(Appointment appointment)
        {
            return false;
        }
    }
}