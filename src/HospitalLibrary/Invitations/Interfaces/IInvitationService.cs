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

        public Task<IEnumerable<Invitation>>CreateEventForAll(CreateInvitationDto createInvitationDto);

        public Task<IEnumerable<Invitation>> CreateEventForSpeciality(CreateInvitationDto createInvitationDto,int specialityId);

        public Invitation DeclineInvitation(int invitationId);
        
    }
}