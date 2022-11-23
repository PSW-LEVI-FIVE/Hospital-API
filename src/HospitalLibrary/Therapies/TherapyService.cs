using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Therapies.Dtos;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Therapies
{
    public class TherapyService : ITherapyService
    {
        private IUnitOfWork _unitOfWork;
        private IBloodStorageService _bloodStorageService;
        private IMedicineService _medicineService;

        public TherapyService(IUnitOfWork unitOfWork, IBloodStorageService bloodStorageService,
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

        public List<BloodConsumptionDTO> GetBloodConsumption()
        {
            List<BloodConsumptionDTO> bloodTherapies = new List<BloodConsumptionDTO>();
            List<Therapy> therapies = _unitOfWork.TherapyRepository.GetAllBloodTherapies();
            foreach (var therapy in therapies)
            {
                BloodTherapy bloodTherapy = (BloodTherapy)therapy;
                BloodConsumptionDTO dto = SetBloodConsumptionDtoValues(therapy, bloodTherapy);
                bloodTherapies.Add(dto);
            }

            return bloodTherapies;
        }

        public List<HospitalizationTherapiesDTO> GetAllHospitalizationTherapies(int hospitalizationId)
        {
            List<HospitalizationTherapiesDTO> therapiesList = new List<HospitalizationTherapiesDTO>();
            List<Therapy> therapies = _unitOfWork.TherapyRepository.GetAllByHospitalization(hospitalizationId).ToList();
            foreach (var therapy in therapies)
            {
                if (therapy.InstanceType.Equals("blood"))
                {
                    BloodTherapy bloodTherapy = (BloodTherapy)therapy;
                    HospitalizationTherapiesDTO dto = SetValuesForBloodTherapies(therapy, bloodTherapy);
                    therapiesList.Add(dto);
                }
                else
                {
                    MedicineTherapy medicineTherapy = (MedicineTherapy)therapy;
                    HospitalizationTherapiesDTO dto = SetValuesForMedicineTherapies(therapy, medicineTherapy);
                    therapiesList.Add(dto);
                }
            }
            return therapiesList;
        }

        private HospitalizationTherapiesDTO SetValuesForBloodTherapies(Therapy therapy, BloodTherapy bTherapy)
        {
            HospitalizationTherapiesDTO dto = new HospitalizationTherapiesDTO();
            dto.Id = therapy.Id;
            dto.TherapyType = therapy.InstanceType;
            dto.Quantity = bTherapy.Quantity;
            dto.PrescribedDate = therapy.GivenAt;
            dto.TypeBlood =(int)bTherapy.BloodType;
            return dto;
        }
        
        private HospitalizationTherapiesDTO SetValuesForMedicineTherapies(Therapy therapy, MedicineTherapy mTherapy)
        {
            HospitalizationTherapiesDTO dto = new HospitalizationTherapiesDTO();
            dto.Id = therapy.Id;
            dto.TherapyType = therapy.InstanceType;
            dto.Quantity = mTherapy.Quantity;
            dto.PrescribedDate = therapy.GivenAt;
            dto.MedicineName = _unitOfWork.MedicineRepository.GetOne(mTherapy.MedicineId).Name;
            return dto;
        }
        
        private BloodConsumptionDTO SetBloodConsumptionDtoValues(Therapy therapy, BloodTherapy bloodTherapy)
        {
            BloodConsumptionDTO dto = new BloodConsumptionDTO();
            dto.Id = bloodTherapy.Id;
            dto.Quantity = bloodTherapy.Quantity;
            dto.TypeBlood = (int)bloodTherapy.BloodType;
            dto.DoctorName = _unitOfWork.DoctorRepository.GetOne(therapy.DoctorId).Name;
            dto.DoctorSurname = _unitOfWork.DoctorRepository.GetOne(therapy.DoctorId).Surname;
            dto.PrescribedDate = therapy.GivenAt;
            return dto;
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