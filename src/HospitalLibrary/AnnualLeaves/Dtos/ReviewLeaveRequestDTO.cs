using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.AnnualLeaves.Dtos
{
    public class ReviewLeaveRequestDTO
    {
        [Required]
        public AnnualLeaveState State { get; set; }

        public string Reason { get; set; }

        public ReviewLeaveRequestDTO(AnnualLeaveState state, string reason) 
        {
            State = state;
            Reason = reason;
        }
        public ReviewLeaveRequestDTO(AnnualLeaveState state)
        {
            State = state;
        }
        public ReviewLeaveRequestDTO() { }

    }
}
