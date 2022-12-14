using System;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class Quantity: ValueObject<Quantity>
    {
        public double Count { get; private set; }

        public Quantity(double count)
        {
            Count = count;
            Validate();
        }

        public Quantity(Quantity quantity)
        {
            Count = quantity.Count;
        }

        private void Validate()
        {
            if (Count < 0)
            {
                throw new Exception("Quantity cannot be less than 0");
            }
        }
        
        protected override bool EqualsCore(Quantity other)
        {
            return Count.Equals(other.Count);
        }

        protected override int GetHashCodeCore()
        {
            int hashCode = Count.GetHashCode();
            return hashCode;
        }
    }
}