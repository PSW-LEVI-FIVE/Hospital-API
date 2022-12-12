using System;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Shared.Model.ValueObjects
{
    
    [Owned]
    public class Address: ValueObject<Address>
    {
        public string Country { get; private set; }
        public string City { get; private set;}
        public string Street { get; private set;}
        public string StreetNumber { get; private set;}

        public Address(string country, string city, string street, string streetNumber)
        {
            Country = country;
            City = city;
            Street = street;
            StreetNumber = streetNumber;
            Validate();
        }
        

        private void Validate()
        {
            if (Country.Trim().Equals("")
                || City.Trim().Equals("")
                || Street.Trim().Equals("")
                || StreetNumber.Trim().Equals(""))
            {
                throw new Exception("All address fields should be filled!");
            }
        }

        protected override bool EqualsCore(Address other)
        {
            return Country.Equals(other.Country) 
                   && City.Equals(other.City) 
                   && Street.Equals(other.Street) 
                   && StreetNumber.Equals(other.StreetNumber);
        }
        
        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Street.GetHashCode();
                hashCode = (hashCode * 397) ^ City.GetHashCode();
                hashCode = (hashCode * 397) ^ Country.GetHashCode();
                hashCode = (hashCode * 397) ^ StreetNumber.GetHashCode();
                return hashCode;
            }
        }

    }
}