using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Repositories
{
    public class RoomEquipmentRepository: BaseRepository<RoomEquipment>, IRoomEquipmentRepository
    {
        public RoomEquipmentRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
        
        public async Task<IEnumerable<RoomEquipment>> GetAllByCombineSearchInRoom(RoomEquipmentDTO roomEquipmentDTO)
        {
            return await _dataContext.RoomEquipment.Where(r => (r.RoomId == roomEquipmentDTO.RoomId)
                                                               && (roomEquipmentDTO.Name.Equals("0") || r.Name.ToLower().Contains(roomEquipmentDTO.Name.ToLower()))
                                                               && (roomEquipmentDTO.Quantity.Equals(0) || r.Quantity >= roomEquipmentDTO.Quantity)).ToListAsync();
        }


        public IEnumerable<Room> CheckFloorByCombineEquipmentSearch(RoomEquipmentDTO roomEquipmentDTO)
        {
           return _dataContext.Rooms.Where(r => r.FloorId.Equals(roomEquipmentDTO.RoomId))
                        .Include(e => e.RoomEquipment
                        .Where(equipment => ((roomEquipmentDTO.Name.Equals("0") || equipment.Name.ToLower().Contains(roomEquipmentDTO.Name.ToLower()))
                        && (roomEquipmentDTO.Quantity.Equals(0) || equipment.Quantity >= roomEquipmentDTO.Quantity)))).ToList();
        }
        public int GetNumberOfUsedEquipment(int equipmentId)
        {

            return _dataContext.EquipmentReallocations
                .Where(a => a.EquipmentId == equipmentId)
                .Where(a => a.state == ReallocationState.PENDING)
                .Select(a => a.amount).Sum();
        }
        public async Task<RoomEquipment> GetEquipmentByRoomAndName(int roomId, string name)
        {
            return await _dataContext.RoomEquipment
                .Where(a => a.RoomId == roomId)
                .Where(a => a.Name == name)
                .SingleOrDefaultAsync();
        }
        public async Task<List<RoomEquipment>> GetEquipmentByRoom(int roomId)
        {
            return await _dataContext.RoomEquipment
                .Where(a => a.RoomId == roomId)
                .ToListAsync();

        }

        public int GetHighestId()
        {
            return _dataContext.RoomEquipment.OrderByDescending(a => a.Id).FirstOrDefault().Id;
        }
    }
    
}