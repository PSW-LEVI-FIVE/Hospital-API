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
        public TimeInterval DateRange { get; set; }
        
        public InvitationStatus InvitationStatus { get; set; }

        
    }
}