using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Medicines;

namespace HospitalLibrary.Allergens
{
    public class Allergen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public List<Medicine> Medicines { get; set; }

        public Allergen(int id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public Allergen() { }
    }
}