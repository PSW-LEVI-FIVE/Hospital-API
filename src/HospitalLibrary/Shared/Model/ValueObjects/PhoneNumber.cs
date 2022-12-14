using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class PhoneNumber: ValueObject<PhoneNumber>
    {
        public string CellNumber { get; private set; }
        
        public PhoneNumber(string cellNumber)
        {
            CellNumber = cellNumber;
        }
        
        public PhoneNumber(PhoneNumber phoneNumber)
        {
            CellNumber = phoneNumber.CellNumber;
        }
        
        private void Validate()
        {
            if (CellNumber.Trim().Equals(""))
            {
                throw new Exception("All address fields should be filled!");
            }
        }
        
        protected override bool EqualsCore(PhoneNumber other)
        {
            Regex validatePhoneNumberRegex = new Regex("^\\+?[1-9][0-9]{7,14}$");
            return validatePhoneNumberRegex.IsMatch(other.CellNumber);;
        }
        
        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = CellNumber.GetHashCode();
                return hashCode;
            }
        }
    }
}