using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Managers
{
    public class ManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DoctorsPopularityDTO> GetMostPopularDoctorInRangeOfAge(int fromAge, int toAge)
        {
            throw new NotImplementedException();
        }

        public List<DoctorsPopularityDTO> GetMostPopularDoctors()
        {
            throw new NotImplementedException();
        }
    }
}
