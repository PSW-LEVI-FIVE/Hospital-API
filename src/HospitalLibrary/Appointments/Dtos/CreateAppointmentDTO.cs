using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Appointments.Dtos
{
    public class CreateAppointmentDTO
    {
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime StartAt { get; set; }
        [Required]        
        public DateTime EndAt { get; set; }

        public CreateAppointmentDTO(int doctorId,int patientId,int roomId,DateTime startAt,DateTime endAt)
        {
            this.DoctorId = doctorId;
            this.PatientId = patientId;
            this.RoomId = roomId;
            this.StartAt = startAt;
            this.EndAt = endAt;
        }

        public Appointment MapToModel()
        {
            return new Appointment
            {
                DoctorId = DoctorId,
                PatientId = PatientId,
                RoomId = RoomId,
                StartAt = StartAt,
                EndAt = EndAt,
                State = AppointmentState.PENDING
            };
        } 
    }
}