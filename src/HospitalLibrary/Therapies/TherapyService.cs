using System.Threading.Tasks;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Therapies
{
    public class TherapyService: ITherapyService
    {
        private IUnitOfWork _unitOfWork;
        private IBloodStorageService _bloodStorageService;
        private IMedicineService _medicineService;

        public TherapyService(IUnitOfWork unitOfWork,IBloodStorageService bloodStorageService,
            IMedicineService medicineService)
        {
            _unitOfWork = unitOfWork;
            _medicineService = medicineService;
            _bloodStorageService = bloodStorageService;
        }
        
        public async Task<BloodTherapy> CreateBloodTherapy(BloodTherapy bloodTherapy)
        {
            if(await ValidateBloodAmount(bloodTherapy.BloodType, bloodTherapy.Quantity));
            _unitOfWork.TherapyRepository.Add(bloodTherapy);
            _unitOfWork.TherapyRepository.Save();
            return bloodTherapy;
        }

        public MedicineTherapy CreateMedicineTherapy(MedicineTherapy medicineTherapy)
        {
            if (ValidateMedicineAmount(medicineTherapy.MedicineId, medicineTherapy.Quantity));
            _unitOfWork.TherapyRepository.Add(medicineTherapy);
            _unitOfWork.TherapyRepository.Save();
            return medicineTherapy;
        }

        private async Task<bool> ValidateBloodAmount(BloodType type, double quantity)
        {
            BloodStorage blood = await _unitOfWork.BloodStorageRepository.GetByType(type);
            bool valid = _bloodStorageService.GiveBlood(blood, quantity);
            return valid;
        }
        
        private bool ValidateMedicineAmount(int id, double quantity)
        {
            bool valid = _medicineService.GiveMedicine(id, quantity);
            return valid;
        }
    }
}