using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Room>> FindAllByFloor(int floor)
        {
            return await _dataContext.Rooms.Where(r => r.FloorId == floor).ToListAsync();
        }
        public async Task<IEnumerable<Room>> GetHospitalExaminationRooms()
        {
            return await _dataContext.Rooms.Where(r => r.RoomType == RoomType.EXAMINATION_ROOM).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetHospitalConsiliumRooms()
        {
            return await _dataContext.Rooms.Where(r => r.RoomType == RoomType.HOSPITAL_ROOM || r.RoomType == RoomType.NO_TYPE ).ToListAsync();
        }

        public async Task<IEnumerable<Room>> SearchByTypeAndName(RoomSearchDTO roomsSearchDTO, int floorId)
        {

            return await _dataContext.Rooms.Where(r =>
                ((r.FloorId == floorId))).Where(r =>
                 (r.RoomNumber.Contains(roomsSearchDTO.RoomName) &&
                 ((r.RoomType == roomsSearchDTO.RoomType) || (roomsSearchDTO.RoomType == RoomType.NO_TYPE)))
                     ).ToListAsync();
        }

        public async Task<IEnumerable<RoomEquipment>> GetAllEquipmentbyRoom(int id)
        {
            return await _dataContext.RoomEquipment.Where(r => r.RoomId == id).ToListAsync();
        }

        public int GetMaxId()
        {
          return _dataContext.Rooms.OrderByDescending(a => a.Id).Select(a => a.Id).FirstOrDefault();

        }
  }
}
