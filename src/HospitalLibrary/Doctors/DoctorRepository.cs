using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<Doctor> GetAllDoctorsWithSpecialityExceptId(SpecialtyType specialtyType, int doctorId)
        {
            return _dataContext.Doctors.Where(doctor => doctor.SpecialtyType.Equals(specialtyType) && doctor.Id != doctorId).ToList();
        }
        public async Task<IEnumerable<Doctor>> GetTwoIternalMedicineDoctorsAscendingByPatientNumber()
        {
            return await _dataContext.Doctors.Where(doctor => doctor.SpecialtyType.Equals(SpecialtyType.ITERNAL_MEDICINE)).
                OrderBy(doctor => doctor.Patients.Count).Include(a => a.Patients).Take(2).ToListAsync();
        }
    }
}