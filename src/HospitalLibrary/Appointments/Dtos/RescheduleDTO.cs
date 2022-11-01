using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Appointments.Dtos
{
    public class RescheduleDTO
    {
        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }
        
    }
}