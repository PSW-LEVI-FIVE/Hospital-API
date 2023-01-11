using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Appointments;

public enum InvitationStatus
{
    ACCEPTED,
    REJECTED,
    PENDING
}
namespace HospitalLibrary.Invitations
{
    public class Invitation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Reason { get; set; }
        public string Place { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        
        public InvitationStatus InvitationStatus { get; set; }

        public Invitation()
        {
        }

        public Invitation(int doctorId, string description, string title, string reason, string place, DateTime startAt, DateTime endAt, InvitationStatus invitationStatus)
        {
            DoctorId = doctorId;
            Description = description;
            Title = title;
            Reason = reason;
            Place = place;
            StartAt = startAt;
            EndAt = endAt;
            InvitationStatus = invitationStatus;
        }

        public Invitation(int id, int doctorId, string description, string title, string reason, string place, DateTime startAt, DateTime endAt, InvitationStatus invitationStatus)
        {
            Id = id;
            DoctorId = doctorId;
            Description = description;
            Title = title;
            Reason = reason;
            Place = place;
            StartAt = startAt;
            EndAt = endAt;
            InvitationStatus = invitationStatus;
        }
    }
}