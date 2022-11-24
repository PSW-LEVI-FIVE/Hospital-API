using System.Collections.Generic;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Medicines.Interfaces
{
    public interface IMedicineRepository: IBaseRepository<Medicine>
    {
        IEnumerable<Medicine> GetCompatibleForPatient(List<int> allergenIds);
    }
}