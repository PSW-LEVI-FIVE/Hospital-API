using System;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Medicines;

namespace HospitalLibrary.Therapies.Model
{
    public class MedicineTherapy: Therapy
    {
        [ForeignKey("Medicine")]
        public int MedicineId { get; private set; }
        public Medicine Medicine { get; private set; }
        public double Quantity { get; private set; }
        
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