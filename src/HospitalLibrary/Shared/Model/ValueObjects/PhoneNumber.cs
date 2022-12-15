using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class PhoneNumber: ValueObject<PhoneNumber>
    {
        public string CellNumber { get; set; }
        
        public PhoneNumber(string cellNumber)
        {
            CellNumber = cellNumber;
            Validate();
        }
        
        public PhoneNumber(PhoneNumber phoneNumber)
        {
            CellNumber = phoneNumber.CellNumber;
            Validate();
        }
        
        private void Validate()
        {
            Regex validatePhoneNumberRegex = new Regex("^\\+?[1-9][0-9]{7,14}$");
            if(!validatePhoneNumberRegex.IsMatch(CellNumber))
            {
                throw new Exception("Invalid phone number");
            }
        }
        
        protected override bool EqualsCore(PhoneNumber other)
        {
            return CellNumber.Equals(other.CellNumber);
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