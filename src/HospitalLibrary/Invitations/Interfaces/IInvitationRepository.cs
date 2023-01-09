using System.Collections.Generic;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Invitations.Interfaces
{
    public interface IInvitationRepository : IBaseRepository<Invitation>
    {
        IEnumerable<Invitation> GetAllInvitations();
        IEnumerable<Invitation> GetAllByDoctorId(int doctorId);
        

    }
}