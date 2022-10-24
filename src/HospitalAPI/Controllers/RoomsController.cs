using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/[controller]")]
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

    }
}