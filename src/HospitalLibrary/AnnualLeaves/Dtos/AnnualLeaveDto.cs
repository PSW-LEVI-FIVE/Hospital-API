using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.AnnualLeaves.Dtos
{
    public class AnnualLeaveDto
    {
        [Required]
        public int DoctorId { get; set; }
        
        [Required]
        public string Reason { get; set; }
        
        [Required]
        public DateTime StartAt { get; set; }
        
        [Required]
        public DateTime EndAt { get; set; }

        [Required]
        public bool IsUrgent { get; set; }
        
        public AnnualLeave MapToModel()
        {
            return new AnnualLeave
            {
                DoctorId = DoctorId,
                EndAt = EndAt,
                IsUrgent = IsUrgent,
                Reason = Reason,
                StartAt = StartAt,
                State = AnnualLeaveState.PENDING
            };
        }
    }
}