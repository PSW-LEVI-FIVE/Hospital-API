using Newtonsoft.Json;

namespace HospitalLibrary.Allergens.Dtos
{
    public class AllergenDTO
    {
        public string Name { get; set; }
        
        public AllergenDTO(string name)
        {
            Name = name;
        }
        public Allergen MapToModel()
        {
            return new Allergen
            {
                Id = 0,
                Name = Name,
            };
        } 
    }
}