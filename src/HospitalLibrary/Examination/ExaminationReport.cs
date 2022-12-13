using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Appointments;
using HospitalLibrary.Doctors;
using HospitalLibrary.Symptoms;

namespace HospitalLibrary.Examination
{
    public class ExaminationReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [ForeignKey("Doctor")] 
        public int DoctorId { get; set;}
        public Doctor Doctor { get; set;}

        public string Content { get; set; }
        
        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }
        public Appointment Examination { get; set; }
        
        public List<Prescription> Prescriptions { get; set; }
        public List<Symptom> Symptoms { get; set; }
        
        public string? Url { get; set; }
        
        public ExaminationReport() {}        
    }
}