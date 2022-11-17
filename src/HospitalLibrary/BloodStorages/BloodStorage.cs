using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.BloodStorages
{
    public enum BloodType
    {
        A_POSITIVE,
        A_NEGATIVE,
        B_POSITIVE,
        B_NEGATIVE,
        AB_POSITIVE,
        AB_NEGATIVE,
        ZERO_POSITIVE,
        ZERO_NEGATIVE
    }

    public class BloodStorage
    {
        [Key] public BloodType BloodType { get; set; }

        public double Quantity { get; set; }

        public BloodStorage(BloodType bloodType, double quantity)
        {
            BloodType = bloodType;
            Quantity = quantity;
        }

        public BloodStorage()
        {
        }
    }
}