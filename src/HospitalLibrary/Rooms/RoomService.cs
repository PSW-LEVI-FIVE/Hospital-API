using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Exceptions;
using SendGrid.Helpers.Errors.Model;
using NotFoundException = HospitalLibrary.Shared.Exceptions.NotFoundException;

namespace HospitalLibrary.Rooms
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork _unitOfWork;
        private readonly ITimeIntervalValidationService _intervalValidation;


        public RoomService(IUnitOfWork unitOfWork,ITimeIntervalValidationService intervalValidation)
        {
            _unitOfWork = unitOfWork;
            _intervalValidation = intervalValidation;
        }

        public Task<IEnumerable<Room>> GetAll()
        {
            return _unitOfWork.RoomRepository.GetAll();
        }

        public Room Update(Room room)
        {
            _unitOfWork.RoomRepository.Update(room);
            _unitOfWork.RoomRepository.Save();
            return room;
        }

        public Room GetOne(int key)
        {
            return _unitOfWork.RoomRepository.GetOne(key);
        }



        public IEnumerable<Bed> GetBedsForRoom(int id)
        {
            return _unitOfWork.BedRepository.GetAllByRoom(id);
        }

        public Room Create(Room room)
        {
            _unitOfWork.RoomRepository.Add(room);
            _unitOfWork.RoomRepository.Save();
            return room;
        }

        public Task<IEnumerable<Room>> SearchRoom(RoomSearchDTO searchRoomDTO,int floorId)
        {

            return _unitOfWork.RoomRepository.SearchByTypeAndName(searchRoomDTO, floorId);
        }
        public async Task<Room> GetFirstAvailableExaminationRoom(TimeInterval choosenInterval)
        {
            IEnumerable<Room> examinationRooms = await _unitOfWork.RoomRepository.GetHospitalExaminationRooms();
            foreach (Room room in examinationRooms)
            {
                bool isOverlapping = await _intervalValidation.IsTimeIntervalOverlapingWithRoomsAppointments(room.Id,choosenInterval);
                if (!isOverlapping)
                    return room;
            }
            throw new NotFoundException("Couldn't find single free room");
        }
        
        public async Task<Room> GetFirstAvailableConsiliumRoom(TimeInterval choosenInterval)
        {
            IEnumerable<Room> examinationRooms = await _unitOfWork.RoomRepository.GetHospitalConsiliumRooms();
            foreach (Room room in examinationRooms)
            {
                bool isOverlapping = await _intervalValidation.IsTimeIntervalOverlapingWithRoomsAppointments(room.Id,choosenInterval);
                if (!isOverlapping)
                    return room;
            }
            throw new NotFoundException("Couldn't find single free room");
        }
        
        public Task<IEnumerable<RoomEquipment>> GetAllEquipmentbyRoomId(int id)
        {
            return _unitOfWork.RoomRepository.GetAllEquipmentbyRoom(id);
        }
    }
}