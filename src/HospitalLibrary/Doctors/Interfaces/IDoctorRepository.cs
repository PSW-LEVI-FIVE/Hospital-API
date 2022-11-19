using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Doctors.Interfaces
{
    public interface IDoctorRepository: IBaseRepository<Doctor>
    {
        IEnumerable<Doctor> GetAllDoctorsWithSpecialityExceptId(SpecialtyType specialtyType, int doctorId);
        Task<IEnumerable<Doctor>> GetTwoUnburdenedDoctors();
    }
}