using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Room> rooms = await _roomService.GetAll();
            return Ok(rooms);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateName(int id, [FromBody] string name)
        {
            Room room = _roomService.GetOne(id);
            room.RoomNumber = name;
            _roomService.Update(room);
            return Ok(room);
        }

    }
}
