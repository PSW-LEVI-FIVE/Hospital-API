using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Allergens;

namespace HospitalLibrary.Medicines
{
    public class Medicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Allergen> Allergens { get; set; }


        public Medicine(int id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public Medicine() {}
    }
}