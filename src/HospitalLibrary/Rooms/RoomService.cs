﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using System.Linq;

namespace HospitalLibrary.Rooms
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork _unitOfWork;


        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public Task<IEnumerable<RoomEquipment>> GetAllEquipmentbyRoomId(int id)
        {
            return _unitOfWork.RoomRepository.GetAllEquipmentbyRoom(id);
        }
    }
}