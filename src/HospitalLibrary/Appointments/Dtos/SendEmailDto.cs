using System.ComponentModel.DataAnnotations;
namespace HospitalLibrary.Appointments.Dtos
{
    public class SendEmailDto
    {
        [Required]
        public string patientEmail { get; set; }
        [Required]
        public string appointmentTime { get; set; }
    }
}