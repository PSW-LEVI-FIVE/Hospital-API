using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Invitations.Dtos;

namespace HospitalLibrary.Invitations.Interfaces
{
    public interface IInvitationService
    {
        IEnumerable<Invitation> GetAllInvitations();
        IEnumerable<Invitation> GetAllByDoctorId(int doctorId);
        public Invitation AcceptInvitation(int invitationId);

        public Task<IEnumerable<Invitation>>CreateEventForAll(Invitation invitation);

        public Task<IEnumerable<Invitation>> CreateEventForSpeciality(Invitation invitation,int specialityId);

        public Invitation DeclineInvitation();
    }
}