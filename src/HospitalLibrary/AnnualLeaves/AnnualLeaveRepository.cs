using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.AnnualLeaves
{
    public class AnnualLeaveRepository : BaseRepository<AnnualLeave>, IAnnualLeaveRepository
    {
        public AnnualLeaveRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<AnnualLeave> GetAllByDoctorId(int doctorId)
        {
            return  _dataContext.AnnualLeaves
                .Where(al => al.State != AnnualLeaveState.DELETED)
                .Where(al =>al.DoctorId == doctorId)
                .OrderBy(al => al.StartAt)
                .ToList();
        }

        public List<int> GetDoctorsThatHaveAnnualLeaveInRange(TimeInterval range)
        {
            return _dataContext.AnnualLeaves
                .Where(al => al.State != AnnualLeaveState.DELETED)
                .Where(a =>
                    range.Start.Date.CompareTo(a.StartAt.Date) <= 0
                    && range.End.Date.CompareTo(a.StartAt.Date) > 0)
                .Select(al => al.DoctorId)
                .ToList();
        }

        public IEnumerable<AnnualLeave> GetDoctorsAnnualLeavesInRange(int doctorId, TimeInterval range)
        {
            return _dataContext.AnnualLeaves
                .Where(al => al.State != AnnualLeaveState.DELETED)
                .Where(a =>
                    a.StartAt.CompareTo(range.Start) <= 0
                    && a.EndAt.Date.CompareTo(range.End) >= 0)
                .Where(al => al.DoctorId == doctorId)
                .Select(al => al)
                .ToList();
        }
    }
}