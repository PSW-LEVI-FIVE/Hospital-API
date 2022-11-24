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
                    (range.Start <= a.StartAt && range.End >= a.EndAt) 
                    || (a.StartAt <= range.Start && a.EndAt >= range.End)
                    || (range.Start <= a.StartAt && range.End >= a.StartAt) 
                    || (range.Start <= a.EndAt && range.End >= a.EndAt)
                    )
                .Select(al => al.DoctorId)
                .ToList();
        }

        public IEnumerable<AnnualLeave> GetDoctorsAnnualLeavesInRange(int doctorId, TimeInterval range)
        {
            return _dataContext.AnnualLeaves
                .Where(al => al.State != AnnualLeaveState.DELETED)
                .Where(a =>
                    (range.Start <= a.StartAt && range.End >= a.EndAt) 
                    || (a.StartAt <= range.Start && a.EndAt >= range.End)
                    || (range.Start <= a.StartAt && range.End >= a.StartAt) 
                    || (range.Start <= a.EndAt && range.End >= a.EndAt)
                )
                .Where(al => al.DoctorId == doctorId)
                .Select(al => al)
                .ToList();
        }
    }
}