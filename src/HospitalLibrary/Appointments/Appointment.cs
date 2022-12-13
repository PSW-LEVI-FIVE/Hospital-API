using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Consiliums;
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

public enum AppointmentType
{
    REGULAR,
    EXAMINATION,
    CONSILIUM
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
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public AppointmentState State { get; set; }
        
        public AppointmentType Type { get; set; }
        
        [ForeignKey("Consilium")]
        public int? ConsiliumId { get; set; }
        public Consilium? Consilium { get; set; }

        public Appointment()
        {
        }

        public Appointment(Appointment appointment)
        {
            Id = appointment.Id;
            DoctorId = appointment.DoctorId;
            Doctor = appointment.Doctor;
            Room = appointment.Room;
            RoomId = appointment.RoomId;
            Patient = appointment.Patient;
            PatientId = appointment.PatientId;
            StartAt = appointment.StartAt;
            State = appointment.State;
            EndAt = appointment.EndAt;
            Type = appointment.Type;
            Consilium = appointment.Consilium;
        }
    }
    
    
}