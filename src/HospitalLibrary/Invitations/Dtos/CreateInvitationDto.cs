using System;

namespace HospitalLibrary.Invitations.Dtos
{
    public class CreateInvitationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        
    }
}