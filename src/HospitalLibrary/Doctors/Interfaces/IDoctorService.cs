using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Doctors.Interfaces
{
    public interface IDoctorService
    {
        Doctor Create(Doctor doctor);
        Task<IEnumerable<Doctor>> GetAll();
    }
}