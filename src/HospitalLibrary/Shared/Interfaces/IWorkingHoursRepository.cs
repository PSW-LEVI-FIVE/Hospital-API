using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface IWorkingHoursRepository : IBaseRepository<WorkingHours>
    {
        WorkingHours GetOne(int day, int doctorId);
    }
}