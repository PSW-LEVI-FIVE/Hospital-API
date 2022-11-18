<<<<<<< HEAD

=======
>>>>>>> 019126d (fixed all comments on PR)
﻿using HospitalLibrary.Managers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Managers.Interfaces
{
    public interface IManagerService
    {
<<<<<<< HEAD
        public IEnumerable<DoctorWithPopularityDTO> GetMostPopularDoctorByAgeRange(int fromAge=0, int toAge=666, bool onlyMostPopularDoctors = false);

=======
        public Task<IEnumerable<DoctorWithPopularityDTO>> GetMostPopularDoctorByAgeRange(int fromAge, int toAge);

        public Task<IEnumerable<DoctorWithPopularityDTO>> GetMostPopularDoctors();
>>>>>>> 019126d (fixed all comments on PR)
    }
}
