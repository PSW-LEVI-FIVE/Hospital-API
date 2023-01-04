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
        public int DoctorId { get; private set;}
        public Doctor Doctor { get; private set;}

        public string? Content { get; private set; }
        
        [ForeignKey("Examination")]
        public int ExaminationId { get; private set; }
        public Appointment Examination { get; private set; }
        
        public List<Prescription> Prescriptions { get; private set; }
        public List<Symptom> Symptoms { get; private set; }
        
        public string? Url { get; private set; }
        
        public ExaminationReport() {}

        public ExaminationReport(int doctorId, string content, int examinationId, string url)
        {
            DoctorId = doctorId;
            Content = content;
            ExaminationId = examinationId;
            Url = url;
        }

        public ExaminationReport(int doctorId, int examinationId)
        {
            DoctorId = doctorId;
            ExaminationId = examinationId;
            Prescriptions = new();
            Symptoms = new();
        }

        public ExaminationReport(int id, int doctorId, List<Prescription> prescriptions, List<Symptom> symptoms, int examinationId, string content)
        {
            Id = id;
            DoctorId = doctorId;
            Prescriptions = prescriptions;
            Symptoms = symptoms;
            ExaminationId = examinationId;
            Content = content;
        }
        
        public ExaminationReport(int id, int doctorId, string content, int examinationId, string url)
        {
            Id = id;
            DoctorId = doctorId;
            Content = content;
            ExaminationId = examinationId;
            Url = url;
        }
        
        public ExaminationReport(int id, int doctorId, string content, int examinationId, string url, List<Prescription> prescriptions, List<Symptom> symptoms)
        {
            Id = id;
            DoctorId = doctorId;
            Content = content;
            ExaminationId = examinationId;
            Url = url;
            Prescriptions = prescriptions;
            Symptoms = symptoms;

        }
        
        public override void Apply(DomainEvent @event)
        {
            Changes.Add(@event);
        }

        public void UpdateAdditional(ExaminationReport report)
        {
            Prescriptions = report.Prescriptions;
            Symptoms = report.Symptoms;
            Content = report.Content;
            Url = report.Url;
        }
    }
}