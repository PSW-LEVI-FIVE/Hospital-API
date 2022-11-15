using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctors;

namespace HospitalLibrary.AnnualLeaves
{
    public enum AnnualLeaveState
    {
        PENDING, APPROVED, DELETED
    }
    
    public class AnnualLeave
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key()]
        public int Id { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        
        public string Reason { get; set; }
        
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public AnnualLeaveState State { get; set; }
        
        public bool IsUrgent { get; set; }

        public AnnualLeave()
        {
            
        }
    }
    
}