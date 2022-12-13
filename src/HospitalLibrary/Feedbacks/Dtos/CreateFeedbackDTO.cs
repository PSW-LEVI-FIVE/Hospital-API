
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Feedbacks.ValueObjects;

namespace HospitalLibrary.Feedbacks.Dtos
{
    public class CreateFeedbackDTO
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public string FeedbackContent { get; set; }

        [Required]
        public FeedbackStatus FeedbackStatus { get; set; }

        public Feedback MapToModel()
        {
            return new Feedback
            {
                PatientId = PatientId,
                FeedbackContent = FeedbackContent,
                FeedbackStatus = FeedbackStatus
            };  
        }


    }
}
