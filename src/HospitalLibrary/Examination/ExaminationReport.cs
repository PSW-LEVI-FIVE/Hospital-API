using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Appointments;
using HospitalLibrary.Doctors;
using HospitalLibrary.Infrastructure.EventSourcing;
using HospitalLibrary.Symptoms;

namespace HospitalLibrary.Examination
{
    public class ExaminationReport: EventSourcedAggregate
    {

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

        public ExaminationReport(int doctorId, string content, int examinationId, string url)
        {
            DoctorId = doctorId;
            Content = content;
            ExaminationId = examinationId;
            Url = url;
        }
        
        public ExaminationReport(int id, int doctorId, string content, int examinationId, string url)
        {
            Id = id;
            DoctorId = doctorId;
            Content = content;
            ExaminationId = examinationId;
            Url = url;
        }
        
        public override void Apply(DomainEvent @event)
        {
            Changes.Add(@event);
        }
    }
}