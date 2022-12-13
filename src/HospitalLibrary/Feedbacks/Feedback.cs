using HospitalLibrary.Patients;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Feedbacks.ValueObjects;

namespace HospitalLibrary.Feedbacks
{
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient  {get;set;}
        public string FeedbackContent { get; set; }
        public FeedbackStatus FeedbackStatus { get; set; }

        public Feedback () { }
    }
}