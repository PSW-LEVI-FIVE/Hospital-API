using System;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Medicines;

namespace HospitalLibrary.Therapies
{
    public class MedicineTherapy: Therapy
    {
        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public double Quantity { get; set; }
        
        public MedicineTherapy(int id, int hospitalizationId, DateTime givenAt, int medicineId, double quantity) : base(id, hospitalizationId, givenAt)
        {
            MedicineId = medicineId;
            Quantity = quantity;
        }
    }
}