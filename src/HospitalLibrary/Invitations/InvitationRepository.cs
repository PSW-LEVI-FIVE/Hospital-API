using System.Collections.Generic;
using HospitalLibrary.Invitations.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Invitations
{
    public class InvitationRepository : BaseRepository<Invitation>, IInvitationRepository
    {
        public  InvitationRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<Invitation> GetAllInvitations()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Invitation> GetAllByDoctorId(int doctorId)
        {
            throw new System.NotImplementedException();
        }
    }
}