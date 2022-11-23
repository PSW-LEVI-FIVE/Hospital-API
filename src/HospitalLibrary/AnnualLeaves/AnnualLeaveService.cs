using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Doctors;

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

        public IEnumerable<PendingRequestsDTO> GetAllPending()
        {
            List<PendingRequestsDTO> pendingRequests = new List<PendingRequestsDTO>();
            foreach (AnnualLeave leave in _unitOfWork.AnnualLeaveRepository.GetAllPending())
            {
                Doctor doctor = _unitOfWork.DoctorRepository.GetOne(leave.DoctorId);
                pendingRequests.Add(new PendingRequestsDTO(leave.Id, leave.DoctorId, doctor.Name + " " + doctor.Surname,
                    leave.Reason, leave.StartAt, leave.EndAt, leave.State, leave.IsUrgent));
            }
            return pendingRequests;
        }

        public async Task<AnnualLeave> Create(AnnualLeave annualLeave)
        {
            await _annualLeaveValidator.Validate(annualLeave);
            _unitOfWork.AnnualLeaveRepository.Add(annualLeave);
            _unitOfWork.AnnualLeaveRepository.Save();
            return annualLeave;
        }
        
        public AnnualLeave Delete(int annualLeaveId,int doctorId)
        {
            AnnualLeave leave=_unitOfWork.AnnualLeaveRepository.GetOne(annualLeaveId);
            _annualLeaveValidator.CancelValidation(leave, doctorId);
            leave.State = AnnualLeaveState.DELETED;
            _unitOfWork.AnnualLeaveRepository.Update(leave);
            _unitOfWork.AnnualLeaveRepository.Save();
            return leave;
        }

        public AnnualLeave ReviewRequest(ReviewLeaveRequestDTO reviewLeaveRequestDTO,int id)
        {
            AnnualLeave leave = _unitOfWork.AnnualLeaveRepository.GetOne(id);
            _annualLeaveValidator.ReviewAnnualLeaveValidation(leave,reviewLeaveRequestDTO);
            leave.State = reviewLeaveRequestDTO.State;
            if(leave.State == AnnualLeaveState.CANCELED)
                leave.Reason = reviewLeaveRequestDTO.Reason;
            _unitOfWork.AnnualLeaveRepository.Update(leave);
            _unitOfWork.AnnualLeaveRepository.Save();
            return leave;
        }
    }
}
