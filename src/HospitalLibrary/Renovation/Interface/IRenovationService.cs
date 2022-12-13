﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Renovation.Interface
{
    public interface IRenovationService
    { 
        Task<List<Model.Renovation>> GetAllPending();
    }
}
