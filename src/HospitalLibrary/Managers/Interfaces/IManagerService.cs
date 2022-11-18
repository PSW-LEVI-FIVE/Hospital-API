using HospitalLibrary.Managers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Managers.Interfaces
{
    public interface IManagerService
    {
        public Task<IEnumerable<DoctorWithPopularityDTO>> GetMostPopularDoctorByAgeRange(int fromAge, int toAge);

        public Task<IEnumerable<DoctorWithPopularityDTO>> GetMostPopularDoctors();
    }
}
