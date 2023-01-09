using System.Collections.Generic;
using HospitalLibrary.Invitations.Interfaces;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Invitations
{
    public class InvitationService : IInvitationService
    {
        private readonly UnitOfWork _unitOfWork;
        public IEnumerable<Invitation> GetAllInvivations()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Invitation> GetAllByDoctorId(int doctorId)
        {
            throw new System.NotImplementedException();
        }
    }
}