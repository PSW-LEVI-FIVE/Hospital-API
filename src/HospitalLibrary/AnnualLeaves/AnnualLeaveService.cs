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

        public AnnualLeaveService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<AnnualLeave>> GetAllByDoctorId(int doctorId)
        {
            throw new System.NotImplementedException();
        }

        public AnnualLeave Create(AnnualLeave annualLeave)
        {
            if (annualLeave.IsValid())
            { 
                _unitOfWork.AnnualLeaveRepository.Add(annualLeave);
                _unitOfWork.AnnualLeaveRepository.Save();
                return annualLeave;
            }
            else
            {
                throw new BadRequestException("Date is Not Valid");
            }
        }
    }
}