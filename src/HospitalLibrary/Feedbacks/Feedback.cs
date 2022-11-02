using HospitalLibrary.Patients;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Boolean AllowPublishment { get; set; }
        public Boolean Published { get; set; }
        public Boolean Anonimity { get; set; }

        public Feedback () { }

        public void Update(bool feedbackPublishmentStatus)
        {
            Published = feedbackPublishmentStatus && AllowPublishment;
        }
    }
}