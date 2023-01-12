namespace HospitalLibrary.Invitations.Dtos
{
    public class DeclineInvitationDto
    {
        public string Reason { get; set; }
        
        public int InvitationId { get; set; }

        public DeclineInvitationDto(string reason, int invitationId)
        {
            Reason = reason;
            InvitationId = invitationId;
        }

        public DeclineInvitationDto()
        {
        }
    }
}