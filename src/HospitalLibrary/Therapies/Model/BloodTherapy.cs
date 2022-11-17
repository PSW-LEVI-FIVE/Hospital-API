using System;
using HospitalLibrary.BloodStorages;

namespace HospitalLibrary.Therapies.Model
{
    public class BloodTherapy: Therapy
    {
        public BloodType BloodType { get; set; }
        public double Quantity { get; set; }
        
        
        public BloodTherapy( int hospitalizationId, DateTime givenAt, BloodType bloodType, double quantity, int doctorId) : base(hospitalizationId, givenAt, doctorId)
        {
            BloodType = bloodType;
            Quantity = quantity;
        }

        public BloodTherapy() : base()
        {
        }
    }
}