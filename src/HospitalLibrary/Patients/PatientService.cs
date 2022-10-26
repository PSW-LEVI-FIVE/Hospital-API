using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Patients
{
    public class PatientService: IPatientService
    {
        private IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Patient>> GetAll()
        {
            return _unitOfWork.PatientRepository.GetAll();
        }
        
        public Patient Create(Patient patient)
        {
            _unitOfWork.PatientRepository.Add(patient);
            _unitOfWork.PatientRepository.Save();
            return patient;
        }
    }
}