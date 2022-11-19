using System.Collections.Generic;
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
            bool valid = await ValidateBloodAmount(bloodTherapy.BloodType, bloodTherapy.Quantity);
            if (valid) 
                _unitOfWork.TherapyRepository.Add(bloodTherapy);
            _unitOfWork.TherapyRepository.Save();
            return bloodTherapy;
        }

        public MedicineTherapy CreateMedicineTherapy(MedicineTherapy medicineTherapy)
        {
            bool valid = ValidateMedicineAmount(medicineTherapy.MedicineId, medicineTherapy.Quantity);
            if (valid)
                _unitOfWork.TherapyRepository.Add(medicineTherapy);
            _unitOfWork.TherapyRepository.Save();
            return medicineTherapy;
        }

        public async Task<List<BloodTherapy>> GetBloodConsumption()
        {
            List<BloodTherapy> bloodTherapies = null;
            List<Therapy> therapies = await _unitOfWork.TherapyRepository.GetAllBloodTherapy();
            foreach (var therapy in therapies)
            {
                BloodTherapy bloodTherapy = (BloodTherapy)therapy;
                bloodTherapies.Add(bloodTherapy);
            }

            return bloodTherapies;
        }

        private async Task<bool> ValidateBloodAmount(BloodType type, double quantity)
        {
            BloodStorage blood = await _unitOfWork.BloodStorageRepository.GetByType(type);
            bool valid = _bloodStorageService.SubtractQuantity(blood, quantity);
            return valid;
        }
        
        private bool ValidateMedicineAmount(int id, double quantity)
        {
            bool valid = _medicineService.SubtractQuantity(id, quantity);
            return valid;
        }
    }
}