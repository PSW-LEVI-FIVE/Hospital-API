using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Renovation.Model
{
    public class TimeSlotReqDTo
    {
        [Required] public DateTime startDate { get; set; }
        [Required] public DateTime endDate { get; set; }
        [Required]
        public int roomId { get; set; }
        [Required]
        public int duration { get; set; }

    }
}
