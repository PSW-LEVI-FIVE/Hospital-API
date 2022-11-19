using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Therapies.Interfaces
{
    public interface ITherapyRepository: IBaseRepository<Therapy>
    {
        IEnumerable<Therapy> GetAllByHospitalization(int hospitalizationId);
        public List<Therapy> GetAllBloodTherapy();
    }
}