using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Patients.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAll();
    }
}