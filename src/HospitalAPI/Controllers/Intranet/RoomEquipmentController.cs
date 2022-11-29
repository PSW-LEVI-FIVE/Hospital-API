using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/equipment")]
    [ApiController]
    public class RoomEquipmentController : ControllerBase
    {
        private IRoomEquipmentService _roomEquipmentService;

        public RoomEquipmentController(IRoomEquipmentService roomEquipmentService)
        {
            _roomEquipmentService = roomEquipmentService;
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> searchEquipmentInRoom([FromQuery] RoomEquipmentDTO roomEquipmentDTO)
        {
            var result = await _roomEquipmentService.searchEquipmentInRoom(roomEquipmentDTO);
            return Ok(result);
        }

        [Route("floorSearch")]
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult searchRoomsByFloorContainingEquipment([FromQuery] RoomEquipmentDTO roomEquipmentDTO)
        {
            var result = _roomEquipmentService.searchRoomsByFloorContainingEquipment(roomEquipmentDTO);
            return Ok(result);
        }


    }
}
