using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        

        [HttpGet]
        [Authorize(Roles="Doctor,Manager")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Room> rooms = await _roomService.GetAll();
            return Ok(rooms);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles="Doctor,Manager")]
        public IActionResult UpdateName(int id, [FromBody] string name)
        {
            Room room = _roomService.GetOne(id);
            room.RoomNumber = name;
            _roomService.Update(room);
            return Ok(room);
        }

        [Route("equipmentView/{roomId:int}")]
        [HttpGet]
       public async Task<IActionResult> GetRoomEquipment(int roomId)
        {
            IEnumerable<RoomEquipment> roomEquipment = await _roomService.GetAllEquipmentbyRoomId(roomId);
            return Ok(roomEquipment);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles="Doctor,Manager")]
        public IActionResult GetbyId(int id)
        {
            Room room = _roomService.GetOne(id);
            return Ok(room);
        }



        [HttpGet]
        [Route("{id}/beds")]
        public IActionResult GetRoomBeds(int id)
        {
            IEnumerable<Bed> beds = _roomService.GetBedsForRoom(id);
            return Ok(beds);
        }


        [Route("search/{id}")]
        [HttpPost]
        public async  Task<IActionResult> SearchRooms(int id, [FromBody] RoomSearchDTO roomSearchDTO)
        {
            Console.WriteLine("hahahahha");
            Console.WriteLine(roomSearchDTO.RoomType);
            Console.WriteLine(roomSearchDTO.RoomName);
            ;
            var rooms =  await _roomService.SearchRoom(roomSearchDTO,id);
            return Ok(rooms);
           //return ;
        }


    }
}
