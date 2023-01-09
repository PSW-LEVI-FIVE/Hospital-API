using System.Collections.Generic;

namespace HospitalLibrary.Invitations.Interfaces
{
    public interface IInvitationService
    {
        IEnumerable<Invitation> GetAllInvivations();
        IEnumerable<Invitation> GetAllByDoctorId(int doctorId);
    }
}