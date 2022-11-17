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
        public double Quantity { get; set; }
        public List<Allergen> Allergens { get; set; }


        public Medicine(int id, string name, double quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }

        public Medicine()
        {
        }
    }
}