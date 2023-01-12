using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Appointments;
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
            return _dataContext.Invitations.Where(r => r.StartAt.CompareTo(DateTime.Now) >= 0);
        }

        public IEnumerable<Invitation> GetAllByDoctorId(int doctorId)
        {
            return _dataContext.Invitations
                .Where(r => r.StartAt.CompareTo(DateTime.Now) >= 0)
                .Where(r => r.InvitationStatus == InvitationStatus.PENDING)
                .Where(r => (r.DoctorId == doctorId));

        }

        public IEnumerable<Invitation> GetDoctorTeamBuildingInvitationsInRange(int doctorId, TimeInterval range)
        {
            return _dataContext.Invitations
                .Where(al => al.InvitationStatus != InvitationStatus.ACCEPTED)
                .Where(a =>
                    (range.Start <= a.StartAt && range.End >= a.EndAt) 
                    || (a.StartAt <= range.Start && a.EndAt >= range.End)
                    || (range.Start <= a.StartAt && range.End >= a.EndAt) 
                    || (range.Start <= a.StartAt && range.End >= a.EndAt)
                )
                .Where(al => al.DoctorId == doctorId)
                .Select(al => al)
                .ToList();
        }
    }
}