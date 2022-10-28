﻿namespace HospitalLibrary.Feedbacks.Dtos
{
    public class AnonymousFeedbackDTO
    {
        public int Id { get; set; }
         public string Patient { get; set; }
         public string FeedbackContent { get; set; }
         
         public AnonymousFeedbackDTO(int id,string patient , string feedbackContent)
         { 
             Id = id;
            Patient = patient;
            FeedbackContent = feedbackContent;
         }
        
    }
}