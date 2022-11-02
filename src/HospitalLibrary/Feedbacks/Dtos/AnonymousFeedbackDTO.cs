namespace HospitalLibrary.Feedbacks.Dtos
{
    public class AnonymousFeedbackDTO
    {
         public string Patient { get; set; }
         public string FeedbackContent { get; set; }
         
         public AnonymousFeedbackDTO(string patient , string feedbackContent)
         {
            Patient = patient;
            FeedbackContent = feedbackContent;
         }
        
    }
}