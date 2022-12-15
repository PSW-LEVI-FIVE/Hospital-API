using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Appointments.Dtos
{
    public class DoctorAppointmentsInRangeDTO
    {
        [Required] public int DoctorId { get; set; }
        [Required] public DateTime DesiredDate { get; set; }
    }
}