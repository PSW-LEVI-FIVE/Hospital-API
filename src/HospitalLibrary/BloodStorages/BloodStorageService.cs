using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.BloodStorages
{
    public class BloodStorageService : IBloodStorageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BloodStorageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool SubtractQuantity(BloodStorage blood, double quantity)
        {
            CheckAmount(blood.Quantity, quantity);
            blood.Quantity -= quantity;
            _unitOfWork.BloodStorageRepository.Update(blood);
            return true;
        }

        public Task<IEnumerable<BloodStorage>> GetAllBloodStorage()
        {
            return _unitOfWork.BloodStorageRepository.GetAll();
        }

        private void CheckAmount(double onStorage, double toTake)
        {
            if (onStorage < toTake)
                throw new BadRequestException("Not enough blood in storage");
        }

        public async Task<BloodStorage> GetByType(BloodType type)
        {
            BloodStorage blood = await _unitOfWork.BloodStorageRepository.GetByType(type);
            return blood;
        }

        public List<BloodType> GetAllCompatibileBloodStorage(int hospitalizationId)
        {
            Hospitalization hospitalization = _unitOfWork.HospitalizationRepository.GetOne(hospitalizationId);
            MedicalRecord medicalRecord = _unitOfWork.MedicalRecordRepository.GetOne(hospitalization.MedicalRecordId);
            Patient patient = _unitOfWork.PatientRepository.GetOne(medicalRecord.PatientId);
            BloodType bType = patient.BloodType;
            List<BloodType> compatibile = FindCompatibile(bType);
            return compatibile;
        }

        private List<BloodType> FindCompatibile(BloodType bType)
        {
            List<BloodType> compatibile = new List<BloodType>();
            compatibile.Add(BloodType.ZERO_NEGATIVE);
            if (bType == BloodType.A_NEGATIVE)
            {
                compatibile.Add(BloodType.A_NEGATIVE);
            }
            else if(bType == BloodType.A_POSITIVE)
            {
                compatibile.Add(BloodType.A_NEGATIVE);
                compatibile.Add(BloodType.A_POSITIVE);
                compatibile.Add(BloodType.ZERO_POSITIVE);
            }
            else if(bType == BloodType.B_POSITIVE)
            {
                compatibile.Add(BloodType.B_NEGATIVE);
                compatibile.Add(BloodType.B_POSITIVE);
                compatibile.Add(BloodType.ZERO_POSITIVE);
            }
            else if(bType == BloodType.B_NEGATIVE)
            {
                compatibile.Add(BloodType.B_NEGATIVE);
            }
            else if(bType == BloodType.AB_POSITIVE)
            {
                compatibile.Add(BloodType.A_POSITIVE);
                compatibile.Add(BloodType.B_POSITIVE);
                compatibile.Add(BloodType.AB_POSITIVE);
                compatibile.Add(BloodType.ZERO_POSITIVE);
                compatibile.Add(BloodType.A_NEGATIVE);
                compatibile.Add(BloodType.B_NEGATIVE);
                compatibile.Add(BloodType.AB_NEGATIVE);
            }
            else if(bType == BloodType.AB_NEGATIVE)
            {
                compatibile.Add(BloodType.A_NEGATIVE);
                compatibile.Add(BloodType.B_NEGATIVE);
                compatibile.Add(BloodType.AB_NEGATIVE);
            }
            else if(bType == BloodType.ZERO_POSITIVE)
            {
                compatibile.Add(BloodType.ZERO_POSITIVE);
            }
            return compatibile;
        }
    }
}