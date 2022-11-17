using System;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Medicines;

namespace HospitalLibrary.Therapies.Model
{
    public class MedicineTherapy: Therapy
    {
        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public double Quantity { get; set; }
        
        public MedicineTherapy(int hospitalizationId, DateTime givenAt, int medicineId, double quantity , int doctorId) : base(hospitalizationId, givenAt, doctorId)
        {
            MedicineId = medicineId;
            Quantity = quantity;
        }

        public MedicineTherapy() : base()
        {
        }
    }
}