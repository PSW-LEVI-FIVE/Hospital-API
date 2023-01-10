using System.Collections.Generic;

namespace HospitalLibrary.Invitations.Interfaces
{
    public interface IInvitationService
    {
        IEnumerable<Invitation> GetAllInvitations();
        IEnumerable<Invitation> GetAllByDoctorId(int doctorId);
        public Invitation AcceptInvitation(int invitationId);

        public Invitation DeclineInvitation();
    }
}