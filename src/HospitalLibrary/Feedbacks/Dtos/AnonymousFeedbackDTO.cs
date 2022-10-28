namespace HospitalLibrary.Feedbacks.Dtos
{
    public class AnonymousFeedbackDTO
    {
         public string Patient { get; set; }
         public string FeedbackContent { get; set; }
         
         public AnonymousFeedbackDTO(string Patient , string FeedbackContent )
         {
            this.Patient = Patient;
            this.FeedbackContent = FeedbackContent;
         }
        
    }
}