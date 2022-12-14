using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Renovation.Interface;
using HospitalLibrary.Rooms.Model;
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

        public async Task<Model.Renovation> Create(Model.Renovation renovation)
        {
            //validate renovation
            _unitOfWork.RenovationRepository.Add(renovation);
            _unitOfWork.RenovationRepository.Save();

            return renovation;
        }
        public void Update(Model.Renovation renovation)
        {
            //validate renovation
            _unitOfWork.RenovationRepository.Update(renovation);
            _unitOfWork.RenovationRepository.Save();
        }
        public async Task Delete(int id)
        {
            _unitOfWork.RenovationRepository.Delete(_unitOfWork.RenovationRepository.GetOne(id));
        }

        public async Task<List<Model.Renovation>> GetAllPending()
        {
            return await _unitOfWork.RenovationRepository.GetAllPending(); 
        }
    }
}
