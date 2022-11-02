using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Appointments.Dtos
{
    public class CalendarStartDTO
    {
        [Required]
        public DateTime Start { get; set; }
    }
}