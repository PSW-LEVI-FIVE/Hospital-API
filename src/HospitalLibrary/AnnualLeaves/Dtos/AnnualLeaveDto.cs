using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Shared.Model.ValueObjects;

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
            return new AnnualLeave(DoctorId, null, Reason, StartAt, EndAt, AnnualLeaveState.PENDING, IsUrgent);
        }
        
        public AnnualLeave MapToModel(int doctorId)
        {
            return new AnnualLeave(doctorId, null, Reason, StartAt, EndAt, AnnualLeaveState.PENDING, IsUrgent);
        }
    }
}