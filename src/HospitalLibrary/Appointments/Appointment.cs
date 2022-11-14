using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Model;

public enum AppointmentState
{
    DELETED,
    FINISHED,
    PENDING
}


namespace HospitalLibrary.Appointments
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key()]
        public int Id { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public AppointmentState State { get; set; }

        public Appointment()
        {
        }
    }
    
    
}