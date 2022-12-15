
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
        public FeedbackStatusDTO FeedbackStatus { get; set; }

        public Feedback MapToModel()
        {
            return new Feedback(PatientId,0, FeedbackContent, new FeedbackStatus(FeedbackStatus));
        }
    }
}
