using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Shared.Dtos
{
    public class AddressDTO
    {
        public string Country { get; set; }
        public string City { get; set;}
        public string Street { get; set;}
        public string StreetNumber { get; set;}
        
        public Address MapToModel()
        {
            return new Address(Country,City,Street,StreetNumber);
        }

        public AddressDTO()
        {
        }

        public AddressDTO(string country, string city, string street, string streetNumber)
        {
            Country = country;
            City = city;
            Street = street;
            StreetNumber = streetNumber;
        }
    }
}