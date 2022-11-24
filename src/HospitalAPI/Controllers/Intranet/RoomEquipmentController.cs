using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
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

        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> searchEquipmentInRoom([FromBody] RoomEquipmentDTO roomEquipmentDTO)
        {
            var result = await _roomEquipmentService.searchEquipmentInRoom(roomEquipmentDTO);
            return Ok(result);
        }

        [Route("floorSearch")]
        [HttpGet]
        public IActionResult searchEquipmentOnFloor([FromBody] RoomEquipmentDTO roomEquipmentDTO)
        {
            var result = _roomEquipmentService.searchEquipmentOnFloor(roomEquipmentDTO);
            return Ok(result);
        }


    }
}
