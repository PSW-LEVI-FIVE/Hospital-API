using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Renovation.Interface;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Renovation
{
    public class RenovationService: IRenovationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RenovationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public async Task<List<Model.Renovation>> GetAllPending()
        {
            return await _unitOfWork.RenovationRepository.GetAllPending(); 
        }
    }
}
