using System.Runtime.InteropServices;

namespace HospitalLibrary.Feedbacks.Dtos
{
    public class ManagersFeedbackDto
    {
        
         public int Id { get; set; }
         public string Patient { get; set; }
         public string FeedbackContent { get; set; }
         public bool AllowPublishment { get; set; }
         public bool Published { get; set; }

         public ManagersFeedbackDto(int id, string patient, string feedbackContent, bool allowPublishment,bool published, bool anonymous)
         {
             if (anonymous)
                 Patient = "Anonymous";
             else
                 Patient = patient;
             Id = id;
             FeedbackContent = feedbackContent;
             AllowPublishment = allowPublishment;
             Published = published;
         }

    }
}