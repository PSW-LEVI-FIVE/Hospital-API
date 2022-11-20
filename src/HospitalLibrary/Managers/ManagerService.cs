
﻿using HospitalLibrary.Appointments;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Managers.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Managers
{
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
