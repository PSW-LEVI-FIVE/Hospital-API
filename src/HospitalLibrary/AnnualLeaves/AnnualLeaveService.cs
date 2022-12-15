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
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Allergens;
using System;
using ceTe.DynamicPDF;
using HospitalLibrary.Migrations;
using HospitalLibrary.Appointments;
using ceTe.DynamicPDF.Merger;
using HospitalLibrary.Shared.Model.ValueObjects;

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
                pendingRequests.Add(new PendingRequestsDTO(leave, doctor));
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
            leave.DeleteAnnualLeave(doctorId);
            _unitOfWork.AnnualLeaveRepository.Update(leave);
            _unitOfWork.AnnualLeaveRepository.Save();
            return leave;
        }

        public AnnualLeave ReviewRequest(ReviewLeaveRequestDTO reviewLeaveRequestDTO,int id)
        {
            AnnualLeave leave = _unitOfWork.AnnualLeaveRepository.GetOne(id);
            if (leave == null)
                throw new NotFoundException("Annual leave with given id doesn't exist!");
            // _annualLeaveValidator.ReviewAnnualLeaveValidation(leave,reviewLeaveRequestDTO);
            leave.ReviewAnnualLeave(reviewLeaveRequestDTO.State, reviewLeaveRequestDTO.Reason);
            _unitOfWork.AnnualLeaveRepository.Update(leave);
            _unitOfWork.AnnualLeaveRepository.Save();
            return leave;
        }

        public IEnumerable<MonthlyLeavesDTO> GetMonthlyStatisticsByDoctorId(int doctorId)
        {
            List<MonthlyLeavesDTO> monthlyLeavesDTOs = new List<MonthlyLeavesDTO>();            
            for(int month = 1; month<=12; month++)
            {
                DateTime firstOfMonth = new DateTime(DateTime.Now.Year, month, 1);
                IEnumerable<AnnualLeave> thisMonthLeaves = _unitOfWork.AnnualLeaveRepository.GetMonthlyLeavesByDoctorId(doctorId, firstOfMonth);
                TimeInterval wholeMonth = new TimeInterval(firstOfMonth, firstOfMonth.AddMonths(1).AddDays(-1));
                int daysCount = 0;
                foreach (AnnualLeave annualLeave in thisMonthLeaves)
                {
                    daysCount += CountSameMonthLeaveDays(annualLeave, wholeMonth);
                }
                monthlyLeavesDTOs.Add(new MonthlyLeavesDTO(firstOfMonth, daysCount));
            } 
            return monthlyLeavesDTOs;
        }

        public int CountSameMonthLeaveDays(AnnualLeave annualLeave, TimeInterval wholeMonth)
        {
            int count = 0;
            DateTime max = annualLeave.EndAt <= wholeMonth.End ? annualLeave.EndAt : wholeMonth.End;
            DateTime min = annualLeave.StartAt >= wholeMonth.Start ? annualLeave.StartAt: wholeMonth.Start;
            for (var d = min; d <= max; d = d.AddDays(1))
            {
                if (!(d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday))
                    count++;
            }
            return count; 
        }

    }
}
