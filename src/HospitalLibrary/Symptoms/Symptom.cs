using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Examination;

namespace HospitalLibrary.Symptoms
{
    public class Symptom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual List<ExaminationReport> ExaminationReports { get; set; }

        
        public Symptom(string name)
        {
            Name = name;
        }
        
        public Symptom() {}
    }
}