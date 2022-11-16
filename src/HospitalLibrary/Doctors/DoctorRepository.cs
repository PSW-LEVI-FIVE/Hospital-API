using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Doctors
{
    public class DoctorRepository: BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext context): base(context) {}
        public async Task<IEnumerable<Doctor>> GetAllDoctorsWithSpecialityExceptId(SpecialtyType specialtyType, int doctorId)
        {
            return await _dataContext.Doctors.Where(doctor => doctor.SpecialtyType.Equals(specialtyType) && doctor.Id != doctorId).ToListAsync();
        }
    }
}