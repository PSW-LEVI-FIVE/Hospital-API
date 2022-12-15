using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using ceTe.DynamicPDF.PageElements;
using HospitalAPI.Storage;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Hospitalizations.Dtos;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Patients;
using HospitalLibrary.PDFGeneration;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Hospitalizations
{
    public class HospitalizationService: IHospitalizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorage _storage;
        private readonly IPDFGenerator _generator;

        public HospitalizationService(IUnitOfWork unitOfWork, IStorage storage, IPDFGenerator generator)
        {
            _unitOfWork = unitOfWork;
            _storage = storage;
            _generator = generator;
        }

        public Hospitalization Create(Hospitalization hospitalization)
        {
            ValidateCreate(hospitalization);
            _unitOfWork.HospitalizationRepository.Create(hospitalization);
            return hospitalization;
        }

        public Hospitalization EndHospitalization(int id, DateTime endTime)
        {
            Hospitalization hospitalization = _unitOfWork.HospitalizationRepository.GetOne(id);
            Hospitalization.ThrowIfNull(hospitalization);
            hospitalization.Finish(endTime);       
            Update(hospitalization);
            return hospitalization;
        }
        
        public async Task<string> GenerateTherapyReport(int id)
        {
            Hospitalization hospitalization = _unitOfWork.HospitalizationRepository.GetOnePopulated(id);
            Hospitalization.ThrowIfNull(hospitalization);
            hospitalization.ValidateForPdfGeneration();
            var pdf = _generator.GenerateTherapyPdf(hospitalization);
            string url = await _storage.UploadFile(pdf, $"hospitalization-{DateTime.Now.ToString("ddMMyyyy")}-{id}");
            hospitalization.UpdatePdfUrl(url);
            Update(hospitalization);
            return url;
        }

        public Task<IEnumerable<Hospitalization>> GetAllForPatient(int id)
        {
            return _unitOfWork.HospitalizationRepository.GetAllForPatient(id);
        }
        
        private void Update(Hospitalization hospitalization)
        {
            _unitOfWork.HospitalizationRepository.UpdateAndSave(hospitalization);
        }
        
        private void ValidateCreate(Hospitalization hospitalization)
        {
            if (!_unitOfWork.MedicalRecordRepository.Exists(hospitalization.MedicalRecordId))
                throw new BadRequestException("Medical record doesn't exist!");
            if (!_unitOfWork.BedRepository.IsBedFree(hospitalization.BedId))
                throw new BadRequestException("Bed is currently taken!");
        }
    }
}