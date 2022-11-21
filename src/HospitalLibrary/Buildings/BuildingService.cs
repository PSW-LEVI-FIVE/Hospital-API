using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Buildings
{
    public class BuildingService: IBuildingService
    {
        private IUnitOfWork _unitOfWork;


        public BuildingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Building>> GetAll()
        {
            return _unitOfWork.BuildingRepository.GetAll();
        }

        public Building Update(Building building)
        {
            _unitOfWork.BuildingRepository.Update(building);
            _unitOfWork.BuildingRepository.Save();
            return building;
        }

        public Building GetOne(int key)
        {
            return _unitOfWork.BuildingRepository.GetOne(key);
        }

        public Building Create(Building building)
        {
            throw new NotImplementedException();
        }
    }
}