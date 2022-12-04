using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Medicines;

namespace HospitalLibrary.Examination
{
    public class Prescription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public string Dose { get; set; }
        
        public virtual List<ExaminationReport> ExaminationReports { get; set; }

        public Prescription(int medicineId, string dose)
        {
            MedicineId = medicineId;
            Dose = dose;
        }
        
        public Prescription() {}
    }
}