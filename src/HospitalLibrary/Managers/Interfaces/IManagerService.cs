
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
        public IEnumerable<DoctorWithPopularityDTO> GetMostPopularDoctorByAgeRange(int fromAge=0, int toAge=666, bool onlyMostPopularDoctors = false);

    }
}
