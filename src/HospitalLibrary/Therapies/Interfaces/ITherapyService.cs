using System.Threading.Tasks;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Therapies.Interfaces
{
    public interface ITherapyService
    {
        Task<BloodTherapy> CreateBloodTherapy(BloodTherapy bloodTherapy);
        
        MedicineTherapy CreateMedicineTherapy(MedicineTherapy medicineTherapy);
    }
}