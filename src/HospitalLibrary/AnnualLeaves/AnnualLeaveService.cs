using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.AnnualLeaves
{
    public class AnnualLeaveService : IAnnualLeaveService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnnualLeaveValidator _annualLeaveValidator;

        public AnnualLeaveService(IUnitOfWork unitOfWork,IAnnualLeaveValidator annualLeaveValidator)
        {
            _unitOfWork = unitOfWork;
            _annualLeaveValidator = annualLeaveValidator;
        }

        public IEnumerable<AnnualLeave> GetAllByDoctorId(int doctorId)
        {
            return _unitOfWork.AnnualLeaveRepository.GetAllByDoctorId(doctorId);
        }

        public async Task<AnnualLeave> Create(AnnualLeave annualLeave)
        {
            _annualLeaveValidator.Validate(annualLeave);
            _unitOfWork.AnnualLeaveRepository.Add(annualLeave);
            _unitOfWork.AnnualLeaveRepository.Save();
            return annualLeave;
        }
    }
}