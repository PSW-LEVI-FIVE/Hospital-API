﻿using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.BloodStorages.Interfaces
{
    public interface IBloodStorageRepository: IBaseRepository<BloodStorage>
    {
        Task<BloodStorage> GetByType(BloodType type);
    }
}