using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [ApiController]
    [Route("api/intranet/beds")]
    public class BedController : ControllerBase
    {
        private IBedService _bedService;

        public BedController(IBedService bedService)
        {
            _bedService = bedService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Bed> beds = await _bedService.GetAll();
            return Ok(beds);
        }

        [HttpGet]
        [Route("room/{id:int}/free")]
        public IActionResult GetAllFreeForRoom(int id)
        {
            IEnumerable<Bed> beds = _bedService.GetAllFreeForRoom(id);
            return Ok(beds);
        }
        
    }
}