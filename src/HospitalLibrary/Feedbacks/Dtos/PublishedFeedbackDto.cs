using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Feedbacks.Dtos
{
    public class PublishedFeedbackDto
    {
        public string Patient { get; set; }

        public string FeedbackContent { get; set; }

        public PublishedFeedbackDto(string patient, string feedbackContent, bool anonymous)
        {
            if (anonymous)
                Patient = "Anonymous";
            else
                Patient = patient;
            FeedbackContent = feedbackContent;
        }
    }
}
