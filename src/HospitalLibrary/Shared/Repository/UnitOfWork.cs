using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Shared.Repository
{
    public class UnitOfWork: IUnitOfWork
    {

        private HospitalDbContext _dataContext;
        private IRoomRepository _roomRepository;

        public UnitOfWork(HospitalDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public IRoomRepository RoomRepository => _roomRepository ?? new RoomRepository(_dataContext);
    }
}