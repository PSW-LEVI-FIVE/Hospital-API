using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ceTe.DynamicPDF.PageElements;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Renovation.Interface
{
    public interface IRenovationRepository : IBaseRepository<Model.Renovation>
    {
        Task<List<Model.Renovation>> GetAllPending();
        Task<List<Model.Renovation>> GetAllPendingForRange(TimeInterval interval);
        Task<TimeInterval> GetLastPendingForDay(DateTime date);
        Task<TimeInterval> GetFirstPendingForDay(DateTime date);
        Task<TimeInterval> GetActiveRenovationForDay(DateTime date);

    }
}