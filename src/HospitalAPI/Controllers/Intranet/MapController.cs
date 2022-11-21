using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/map")]
    [ApiController]
    [Authorize(Roles="Manager")]
    public class MapController: ControllerBase
    {
        private IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }

        [HttpGet]
        [Route("buildings")]
        public async Task<IActionResult> GetAllBuildings()
        {
            IEnumerable<MapBuilding> mapBuildings = await _mapService.GetAllBuildings();
            return Ok(mapBuildings);
        }
        
        [HttpGet]
        [Route("floors/{id}")]
        public async Task<IActionResult> GetFloorsByBuilding(int id)
        {
            IEnumerable<MapFloor> mapFloors = await _mapService.GetFloorsByBuilding(id);
            return Ok(mapFloors);
        }
        
        [HttpGet]
        [Route("rooms/{id}")]
        public async Task<IActionResult> GetRoomsByBuilding(int id)
        {
            IEnumerable<MapRoom> mapRooms = await _mapService.GetRoomsByFloor(id);
            return Ok(mapRooms);
        }

    }
}