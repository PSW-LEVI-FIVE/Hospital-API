using System;

namespace HospitalLibrary.Core.Repository.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRoomRepository RoomRepository { get; }
    }
}