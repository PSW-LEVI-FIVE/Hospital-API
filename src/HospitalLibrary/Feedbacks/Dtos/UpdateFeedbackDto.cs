using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Feedbacks.Dtos
{
    public class UpdateFeedbackDto
    {
        public string FeedbackContent { get; set; }
        public Boolean AllowPublishment { get; set; }
        public Boolean Published { get; set; }
        public Boolean Anonimity { get; set; }
    }
}
