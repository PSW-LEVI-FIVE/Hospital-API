using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Hospitalizations.Interfaces
{
    public interface IHospitalizationRepository: IBaseRepository<Hospitalization>
    {
        public Hospitalization GetOnePopulated(int id);
        public Task<IEnumerable<Hospitalization>> GetAllForPatient(int id);
    }
}