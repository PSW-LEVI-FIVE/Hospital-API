using System;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Rooms.Interfaces;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRoomRepository RoomRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
    }
}