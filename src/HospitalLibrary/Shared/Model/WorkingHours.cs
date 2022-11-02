using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctors;

namespace HospitalLibrary.Shared.Model
{
    public class WorkingHours
    {
        [Column(Order = 0), Key]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        
        [Column(Order = 1), Key]
        public int Day { get; set; }
        
        public TimeSpan Start { get; set; }
        
        public TimeSpan End { get; set; }
        
        public WorkingHours(int doctorId, int day, TimeSpan start, TimeSpan end)
        {
            DoctorId = doctorId;
            Day = day;
            Start = start;
            End = end;
        }
    }
}

