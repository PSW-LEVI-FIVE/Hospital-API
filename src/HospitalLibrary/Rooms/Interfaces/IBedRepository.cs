﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IBedRepository: IBaseRepository<Bed>
    {
       IEnumerable<Bed> GetAllFreeBedsForRoom(int roomId);
        bool IsBedFree(int bedId);

        IEnumerable<Bed> GetAllByRoom(int roomId);
    }
}