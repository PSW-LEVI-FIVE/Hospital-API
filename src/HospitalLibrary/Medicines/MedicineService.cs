using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Medicines
{
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool SubtractQuantity(int id, double quantity)
        {
            Medicine medicine = _unitOfWork.MedicineRepository.GetOne(id);
            CheckAmount(medicine.Quantity, quantity);
            medicine.Quantity -= quantity;
            _unitOfWork.MedicineRepository.Update(medicine);
            return true;
        }
        
        public Task<IEnumerable<Medicine>> getAllMedicine()
        {
            return _unitOfWork.MedicineRepository.GetAll();
        }

        public IEnumerable<Medicine> getAllCompatibileMedicine(int hospitalizationId)
        {
            Hospitalization hospitalization = _unitOfWork.HospitalizationRepository.GetOne(hospitalizationId);
            MedicalRecord medicalRecord = _unitOfWork.MedicalRecordRepository.GetOne(hospitalization.MedicalRecordId);
            List<int> allergenIds = _unitOfWork.AllergenRepository.GetAllergenIdsByPatient(medicalRecord.PatientId);
            IEnumerable<Medicine> compatibile = _unitOfWork.MedicineRepository.GetCompatibleForPatient(allergenIds);
            return compatibile;
        }
        

        private void CheckAmount(double onStorage, double toTake)
        {
            if (onStorage < toTake)
                throw new BadRequestException("Not enough medicine in storage");
        }
    }
}