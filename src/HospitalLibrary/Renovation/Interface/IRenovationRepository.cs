using System.Collections.Generic;
using System.Threading.Tasks;
using ceTe.DynamicPDF.PageElements;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Renovation.Interface
{
    public interface IRenovationRepository : IBaseRepository<Model.Renovation>
    {
        Task<List<Model.Renovation>> GetAllPending();
    }
}