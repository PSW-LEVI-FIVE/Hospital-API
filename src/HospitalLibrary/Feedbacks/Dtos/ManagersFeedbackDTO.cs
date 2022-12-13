
using HospitalLibrary.Feedbacks.ValueObjects;

namespace HospitalLibrary.Feedbacks.Dtos
{
    public class ManagersFeedbackDTO
    {
        
         public int Id { get; set; }
         public string Patient { get; set; }
         public string FeedbackContent { get; set; }
         public FeedbackStatus FeedbackStatus { get; set; }

         public ManagersFeedbackDTO(int id, string patient, string feedbackContent, FeedbackStatus feedbackStatus, bool anonymous)
         {
             if (anonymous)
                 Patient = "Anonymous";
             else
                 Patient = patient;
             Id = id;
             FeedbackContent = feedbackContent;
             FeedbackStatus = feedbackStatus;
         }

    }
}