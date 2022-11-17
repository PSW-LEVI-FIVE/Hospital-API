namespace HospitalLibrary.Allergens.Dtos
{
    public class CreateAllergenDTO
    {
        public string Name { get; set; }
        
        public CreateAllergenDTO(string name)
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