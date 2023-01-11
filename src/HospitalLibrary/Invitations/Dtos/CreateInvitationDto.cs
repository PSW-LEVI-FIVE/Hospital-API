using System;

namespace HospitalLibrary.Invitations.Dtos
{
    public class CreateInvitationDto
    {
        public  string Title { get; set; }
        public  string Description { get; set; }
        public  string Place { get; set; }
        public  DateTime StartAt { get; set; }
        public  DateTime EndAt { get; set; }
        public InvitationStatus InvitationStatus { get; set; }
        public int DoctorId { get; set; }
        public int SpecialityId { get; set; }
    
    
        public  Invitation MapToModel()
        {
            return new Invitation
            {
                StartAt = StartAt,
                EndAt = EndAt,
                InvitationStatus = InvitationStatus.PENDING,
                Description = Description,
                Title = Title,
                Place = Place
                
            };
        }
        
    }
}