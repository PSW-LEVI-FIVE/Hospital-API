using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
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
