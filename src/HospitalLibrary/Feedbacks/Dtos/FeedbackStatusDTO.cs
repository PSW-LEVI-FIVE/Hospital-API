using System;

namespace HospitalLibrary.Feedbacks.Dtos
{
    public class FeedbackStatusDTO
    {
        public Boolean AllowPublishment { get; set; }
        public Boolean Published { get; set; }
        public Boolean Anonimity { get; set; }

        public FeedbackStatusDTO()
        {
        }

        public FeedbackStatusDTO(bool allowPublishment, bool published, bool anonimity)
        {
            AllowPublishment = allowPublishment;
            Published = published;
            Anonimity = anonimity;
        }
    }
}