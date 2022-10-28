<<<<<<< HEAD
﻿using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Patients;
using Microsoft.EntityFrameworkCore.Update.Internal;
=======
﻿using HospitalLibrary.Patients;
>>>>>>> 64352f55ca88e95cc7d593f1b2d127dfbc949313
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

        public void Update(UpdateFeedbackDto updateFeedbackDto)
        {
            this.FeedbackContent = updateFeedbackDto.FeedbackContent;
            this.AllowPublishment = updateFeedbackDto.AllowPublishment;
            this.Published = updateFeedbackDto.Published;
            this.Anonimity = updateFeedbackDto.Anonimity;
        }
    }
}