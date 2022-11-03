using HospitalLibrary.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Feedbacks.Dtos
{
    public class CreateFeedbackDTO
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public string FeedbackContent { get; set; }
        [Required]
        public Boolean AllowPublishment { get; set; }
        [Required]
        public Boolean Published { get; set; }
        [Required]
        public Boolean Anonimity { get; set; }

        public Feedback MapToModel()
        {
            return new Feedback
            {
                PatientId = PatientId,
                FeedbackContent = FeedbackContent,
                AllowPublishment = AllowPublishment,
                Published = Published,
                Anonimity = Anonimity
            };  
        }


    }
}
