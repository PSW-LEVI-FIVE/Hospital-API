using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Consiliums;
using HospitalLibrary.Doctors;
using HospitalLibrary.Infrastructure.EventSourcing;
using HospitalLibrary.Patients;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Model;

public enum AppointmentState
{
    DELETED,
    FINISHED,
    PENDING,
    NOT_CREATED
}

public enum AppointmentType
{
    REGULAR,
    EXAMINATION,
    CONSILIUM
}


namespace HospitalLibrary.Appointments
{
    public class Appointment : EventSourcedAggregate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key()]
        public int Id { get; set; }
        
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        
        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }
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

        public Appointment(int patientId)
        {
            this.PatientId = patientId;
            this.State = AppointmentState.NOT_CREATED;
            this.Type = AppointmentType.REGULAR;
        }

        public Appointment(int id, int doctorId, int patientId,int roomId,DateTime startAt,DateTime endAt,AppointmentState state,AppointmentType type)
        {
            Id = id;
            DoctorId = doctorId;
            PatientId = patientId;
            RoomId = roomId;
            StartAt = startAt;
            EndAt = endAt;
            State = state;
            Type = type;
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
        public override void Apply(DomainEvent @event)
        {
            Changes.Add(@event);
        }
    }
    
    
    
}