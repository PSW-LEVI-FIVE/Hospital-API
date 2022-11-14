using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Rooms.Repositories
{
    public class RoomEquipmentRepository: BaseRepository<RoomEquipment>, IRoomEquipmentRepository
    {
        public RoomEquipmentRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}