using System.ComponentModel.DataAnnotations;
namespace HospitalLibrary.Appointments.Dtos
{
    public class AppointmentCancelledDTO
    {
        [Required]
        public string PatientEmail { get; set; }
        [Required]
        public string AppointmentTime { get; set; }
    }
}