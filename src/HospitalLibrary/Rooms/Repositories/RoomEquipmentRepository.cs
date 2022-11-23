using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repositories
{
    public class RoomEquipmentRepository: BaseRepository<RoomEquipment>, IRoomEquipmentRepository
    {
        public RoomEquipmentRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<RoomEquipment>> GetAllByQuantitySearchInRoom(RoomEquipmentDTO roomEquipmentDTO)
        {
            return await _dataContext.RoomEquipment.Where(r => r.Quantity >= roomEquipmentDTO.Quantity
                                                            && r.RoomId.Equals(roomEquipmentDTO.RoomId)).ToListAsync();
        }

        public async Task<IEnumerable<RoomEquipment>> GetAllByNameSearchInRoom(RoomEquipmentDTO roomEquipmentDTO)
        {
            return await _dataContext.RoomEquipment.Where(r => r.Name.ToLower().Contains(roomEquipmentDTO.Name.ToLower())
                                                          && r.RoomId.Equals(roomEquipmentDTO.RoomId)).ToListAsync();
        }

        public async Task<IEnumerable<RoomEquipment>> GetAllByCombineSearchInRoom(RoomEquipmentDTO roomEquipmentDTO)
        {
            return await _dataContext.RoomEquipment.Where(r => (r.RoomId == roomEquipmentDTO.RoomId) &&
                                                                 (r.Name.ToLower().Contains(roomEquipmentDTO.Name.ToLower())) &&
                                                                 (r.Quantity >= roomEquipmentDTO.Quantity)).ToListAsync();
        }

        public bool checkFloorByEquipmentName(Task<IEnumerable<RoomEquipment>> roomEquipment, RoomEquipmentDTO roomEquipmentDTO)
        {
            foreach (var equipment in roomEquipment.Result)
            {
                if (equipment.Name.ToLower().Contains(roomEquipmentDTO.Name.ToLower()))

                    return true;
            }
            return false;
        }

        public bool checkFloorByEquipmentQuantity(Task<IEnumerable<RoomEquipment>> roomEquipment, RoomEquipmentDTO roomEquipmentDTO)
        {
            foreach (var equipment in roomEquipment.Result)
            {
                if (equipment.Quantity >= roomEquipmentDTO.Quantity)
                
                    return true;
            }
            return false;
        }

        public bool checkFloorByCombineEquipmentSearch(Task<IEnumerable<RoomEquipment>> roomEquipment, RoomEquipmentDTO roomEquipmentDTO)
        {
            foreach (var equipment in roomEquipment.Result)
            {
                if (equipment.Quantity >= roomEquipmentDTO.Quantity && equipment.Name.ToLower().Contains(roomEquipmentDTO.Name.ToLower()))

                    return true;
            }
            return false;
        }
        public int GetNumberOfUsedEquipment(int equipmentId)
        {
            
            return _dataContext.EquipmentReallocations
                .Where(a => a.EquipmentId == equipmentId)
                .Where(a=> a.State==ReallocationState.PENDING)
                .Select(a => a.Amount).Sum();
        }

        public async Task<List<RoomEquipment>> GetEquipmentByRoom(int roomId)
        {
            return await _dataContext.RoomEquipment
                .Where(a => a.RoomId==roomId )
                .ToListAsync();

        }
    }
    
}