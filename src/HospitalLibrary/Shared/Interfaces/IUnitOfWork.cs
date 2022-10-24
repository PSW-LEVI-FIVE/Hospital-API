using System;
using HospitalLibrary.Rooms.Interfaces;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRoomRepository RoomRepository { get; }
    }
}