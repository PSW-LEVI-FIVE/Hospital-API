using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Floors.Dtos;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/map")]
    [ApiController]
    [Authorize(Roles="Manager")]
    public class MapController: ControllerBase
    {
        private IMapService _mapService;
        private IRoomService _roomService;

        public MapController(IMapService mapService, IRoomService roomService)
        {
            _mapService = mapService;
            _roomService = roomService;
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

        [HttpPost]
        [Route("rooms")]
        public IActionResult CreateRoom([FromBody] CreateRoomDto createRoomDto)
        {
            MapFloor mapFloor = _mapService.GetFloorById(createRoomDto.MapFloorId);

            Room room = createRoomDto.DtoToRoom();
            room.FloorId = mapFloor.FloorId;

            room = _roomService.Create(room);
            if (room == null) return Problem("Error: room was not created");

            MapRoom mapRoom = createRoomDto.DtoToMapRoom();
            mapRoom.RoomId = room.Id;

            mapRoom = _mapService.CreateRoom(mapRoom);
            return Ok(mapRoom);
        }

        [HttpPost]
        [Route("floors")]
        public IActionResult CreateFloor([FromBody] CreateFloorDto createFloorDto)
        {
            return Ok(null);
        }
    }
}