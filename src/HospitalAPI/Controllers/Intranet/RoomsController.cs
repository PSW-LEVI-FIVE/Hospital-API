using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Interfaces;
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
        private IAppointmentService _appointmentService;
        private IEquipmentReallocationService _reallocationService;
        public RoomsController(IRoomService roomService, IAppointmentService appointmentService,IEquipmentReallocationService equipmentReallocationService)
        {
            _roomService = roomService;
            _appointmentService = appointmentService;
            _reallocationService = equipmentReallocationService;

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


        [Route("equipment/room/{roomId:int}")]
        [HttpGet]
        [Authorize(Roles = "Manager")]
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


        [Route("search/{floorId}")]
        [HttpPost]
        public async  Task<IActionResult> SearchRooms(int floorId, [FromBody] RoomSearchDTO roomSearchDTO)
        {
            
            var rooms =  await _roomService.SearchRoom(roomSearchDTO,floorId);
            return Ok(rooms);
          
        }

        [Route("schedule/appointment/{roomId}")]
        [HttpGet]
        public async Task<IActionResult> GetRoomSchedule(int roomId)
        {
            var appointments = await _appointmentService.GetUpcomingAppointmentsForRoom(roomId);
            
            return Ok(appointments);
        }
        
        [Route("schedule/relocation/{roomId}")]
        [HttpGet]
        public async Task<IActionResult> GetRoomEquipmentRelocation(int roomId)
        {
            var equipmentRelocations = await _reallocationService.GetAllPendingForSpecificRoom(roomId);
            return Ok(equipmentRelocations);
        }
        
        [Route("schedule/renovation/{roomId}")]
        [HttpGet]
        public async Task<IActionResult> GetRoomRenovation(int roomId)
        {
            var equipmentRelocations = await _reallocationService.GetAllPendingForSpecificRoom(roomId);
            return Ok(equipmentRelocations);
        }

        [Route("schedule/cancel/relocation")]
        [HttpPost]

        public  IActionResult CancelEquipmentRelocation(int equipmentReallocationId)
        {
            EquipmentReallocation reallocation =   _reallocationService.CancelEquipmentRelocation(equipmentReallocationId);
            return Ok();

        }
        
        
        
        


    }
}
