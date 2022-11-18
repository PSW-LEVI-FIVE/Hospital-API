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
        private IUnitOfWork _unitOfWork;
        private IHospitalizationValidator _validator;
        private IStorage _storage;
        private IPDFGenerator _generator;

        public HospitalizationService(IUnitOfWork unitOfWork, IHospitalizationValidator validator, IStorage storage, IPDFGenerator generator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _storage = storage;
            _generator = generator;
        }

        public Hospitalization Create(Hospitalization hospObj)
        {
            _validator.ValidateCreate(hospObj);
            _unitOfWork.HospitalizationRepository.Add(hospObj);
            _unitOfWork.HospitalizationRepository.Save();
            return hospObj;
        }

        public Hospitalization EndHospitalization(int id, EndHospitalizationDTO dto)
        {
            Hospitalization hospitalization = _unitOfWork.HospitalizationRepository.GetOne(id);
            _validator.ValidateEndHospitalization(hospitalization, dto);

            hospitalization.State = HospitalizationState.FINISHED;
            hospitalization.EndTime = dto.EndTime;
            
            _unitOfWork.HospitalizationRepository.Update(hospitalization);
            _unitOfWork.HospitalizationRepository.Save();
            return hospitalization;
        }

        public async Task<string> GenerateTherapyReport(int id)
        {
            Hospitalization hospitalization = _unitOfWork.HospitalizationRepository.GetOnePopulated(id);
            ValidateHospitalizationForPdfGeneration(hospitalization);
            MedicalRecord record = _unitOfWork.MedicalRecordRepository.GetOne(hospitalization.MedicalRecordId); 
            Patient patient = _unitOfWork.PatientRepository.GetOne(record.PatientId);
            IEnumerable<Therapy> therapies = _unitOfWork.TherapyRepository.GetAllByHospitalization(id);
            hospitalization.Therapies = therapies.ToList();
            byte[] pdf = _generator.GenerateTherapyPdf(hospitalization, patient);
            return await _storage.UploadFile(pdf, "hospitalization-" + DateTime.Now + "-" + id);
        }

        private void ValidateHospitalizationForPdfGeneration(Hospitalization hospitalization)
        {
            if (hospitalization == null) throw new NotFoundException("Hospitalization with given id doesnt exist!");
            if (hospitalization.State != HospitalizationState.FINISHED) throw new BadRequestException("Hospitalization should be finished!");
            if (!hospitalization.PdfUrl.Trim().Equals("")) throw new BadRequestException("Report already generated for given hospitalization!");
        }
        
    }
}