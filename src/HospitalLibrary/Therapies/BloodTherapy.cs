using System;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Hospitalizations;

namespace HospitalLibrary.Therapies
{
    public class BloodTherapy: Therapy
    {
        public BloodType BloodType { get; set; }
        public double Quantity { get; set; }


        public BloodTherapy(int id, int hospitalizationId, DateTime givenAt) : base(id, hospitalizationId, givenAt)
        {
        }
        
        public BloodTherapy(int id, int hospitalizationId, DateTime givenAt, BloodType bloodType, double quantity) : base(id, hospitalizationId, givenAt)
        {
            BloodType = bloodType;
            Quantity = quantity;
        }
    }
}