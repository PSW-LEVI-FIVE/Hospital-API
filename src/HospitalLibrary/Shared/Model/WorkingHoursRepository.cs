using System.Linq;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Shared.Model
{
    public class WorkingHoursRepository : BaseRepository<WorkingHours>, IWorkingHoursRepository
    {
        public WorkingHoursRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public WorkingHours GetOne(int day, int doctorId)
        {
            return _dataContext.WorkingHours.Single(w => w.Day.Equals(day) && w.DoctorId.Equals(doctorId));
        }
    }
}