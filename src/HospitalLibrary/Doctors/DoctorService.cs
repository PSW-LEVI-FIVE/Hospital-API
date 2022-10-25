using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Doctors
{
    public class DoctorService: IDoctorService
    {
        private IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
        public Doctor Create(Doctor doctor)
        {
            _unitOfWork.DoctorRepository.Add(doctor);
            _unitOfWork.DoctorRepository.Save();
            return doctor;
        }

        public Task<IEnumerable<Doctor>> GetAll()
        {
            return _unitOfWork.DoctorRepository.GetAll();
        }
    }
}