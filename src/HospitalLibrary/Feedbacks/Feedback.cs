using HospitalLibrary.Patients;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Feedbacks.ValueObjects;
using HospitalLibrary.Shared.Model;
using Microsoft.Extensions.Options;

namespace HospitalLibrary.Feedbacks
{
    public class Feedback:BaseEntity
    {
        [ForeignKey("Patient")]
        public int PatientId { get;private set; }
        public Patient Patient  {get;private set;}
        public string FeedbackContent { get;private set; }
        public FeedbackStatus FeedbackStatus { get;private set; }
        public Feedback () { }

        public Feedback(string feedbackContent, FeedbackStatus feedbackStatus)
        {
            this.FeedbackContent = feedbackContent;
            this.FeedbackStatus = feedbackStatus;
            Validate();
        }
        public Feedback(int patientId, int feedbackId, string feedbackContent, FeedbackStatus feedbackStatus)
        {
            this.Id = feedbackId;
            this.PatientId = patientId;
            this.FeedbackContent = feedbackContent;
            this.FeedbackStatus = feedbackStatus;
            Validate();
        }

        private void Validate()
        {
            if (FeedbackContent.Equals(""))
            {
                throw new Exception("Feedback content is empty!");
            }
        }
    }
}