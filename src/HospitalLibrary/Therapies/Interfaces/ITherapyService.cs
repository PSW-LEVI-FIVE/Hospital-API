using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Therapies.Dtos;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Therapies.Interfaces
{
    public interface ITherapyService
    {
        Task<BloodTherapy> CreateBloodTherapy(BloodTherapy bloodTherapy);
        
        MedicineTherapy CreateMedicineTherapy(MedicineTherapy medicineTherapy);

        List<BloodConsumptionDTO> GetBloodConsumption();

        List<HospitalizationTherapiesDTO> GetAllHospitalizationTherapies(int hospitalizationId);
    }
}