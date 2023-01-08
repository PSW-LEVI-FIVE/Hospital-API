using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class Area : ValueObject<Area>
    {
        public float Measure { get; private set; }

        public Area(float measure)
        {
            Measure = measure;
            Validate();
        }

        private void Validate()
        {
            if (Measure < 0)
            {
                throw new Exception("Area cannot be less than 0");
            }

        }

        protected override bool EqualsCore(Area other)
        {
            return Measure.Equals(other.Measure);
        }

        protected override int GetHashCodeCore()
        {
            int hashCode = Measure.GetHashCode();
            return hashCode;
        }
    }
}
