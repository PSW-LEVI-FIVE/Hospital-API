using System;
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

        public Doctor(int id, string name, string surname, string email, string uid, string phoneNumber, DateTime birthDate, string address, SpecialtyType specialtyType) : base(id, name, surname, email, uid, phoneNumber, birthDate, address)
        {
            SpecialtyType = specialtyType;
        }
    }
}