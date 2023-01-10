using System.Collections.Generic;
using HospitalLibrary.Invitations.Interfaces;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Invitations
{
    public class InvitationService : IInvitationService
    {
        private readonly UnitOfWork _unitOfWork;
        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _unitOfWork.InvitationRepository.GetAllInvitations();
        }

        public IEnumerable<Invitation> GetAllByDoctorId(int doctorId)
        {
            return _unitOfWork.InvitationRepository.GetAllByDoctorId(doctorId);
        }

        public Invitation AcceptInvitation(int invitationId)
        {
            Invitation invitation=_unitOfWork.InvitationRepository.GetOne(invitationId);
            invitation.InvitationStatus = InvitationStatus.ACCEPTED;
            _unitOfWork.InvitationRepository.Update(invitation);
            return invitation;
        }

        public Invitation DeclineInvitation()
        {
            throw new System.NotImplementedException();
        }
    }
}