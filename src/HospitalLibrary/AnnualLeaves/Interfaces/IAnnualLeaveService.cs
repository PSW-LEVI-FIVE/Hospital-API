﻿using HospitalLibrary.Allergens;
using HospitalLibrary.AnnualLeaves.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.AnnualLeaves.Interfaces
{
    public interface IAnnualLeaveService
    {
        IEnumerable<AnnualLeave> GetAllByDoctorId(int doctorId);

        IEnumerable<PendingRequestsDTO> GetAllPending();

        Task<AnnualLeave> Create(AnnualLeave annualLeave);

        AnnualLeave Delete(int annualLeaveId, int doctorId);

        AnnualLeave ReviewRequest(ReviewLeaveRequestDTO reviewLeaveRequestDTO, int id);
        IEnumerable<MonthlyLeavesDTO> GetMonthlyStatisticsByDoctorId(int doctorId);
    }
}