using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Allergens;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Medicines
{
    public class Medicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Name Name { get; set; }
        public double Quantity { get; set; }
        public List<Allergen> Allergens { get; set; }


        public Medicine(int id, Name name, double quantity)
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