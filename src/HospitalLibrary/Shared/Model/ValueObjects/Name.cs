using System;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class Name : ValueObject<Name>
    {
        public string NameString { get;private set; }


        public Name(string nameString)
        {
            NameString = nameString;
        }

        public Name(Name name)
        {
            NameString = name.NameString;
            Validate();
        }
        private void Validate()
        {
            if (NameString == null || NameString.Trim().Equals(""))
                throw new Exception("Medicine name should not be empty!");
            
        }

        protected override bool EqualsCore(Name other)
        {
            return NameString.Equals(other.NameString);
        }

        protected override int GetHashCodeCore()
        {
            int hashCode = NameString.GetHashCode();
            return hashCode;
        }
    }
}