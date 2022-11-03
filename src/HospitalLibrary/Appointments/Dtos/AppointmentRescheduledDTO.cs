using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Appointments.Dtos
{
    public class AppointmentRescheduledDTO
    {
        [Required] public string PatientEmail { get; set; }
        [Required] public DateTime AppointmentTimeBefore { get; set; }
    }
}