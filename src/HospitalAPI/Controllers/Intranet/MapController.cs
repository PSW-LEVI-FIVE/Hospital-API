using System.Collections.Generic;
using System.Threading.Tasks;
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
        public IActionResult Create([FromBody] CreateRoomDto createRoomDto)
        {
            //make room
            Room room = new Room
            {
                RoomNumber = createRoomDto.RoomNumber,
                Area = createRoomDto.Area,
                FloorId = createRoomDto.MapFloorId,
            };
            
            room = _roomService.Create(room);
            if (room == null)
            {
                return Problem("Error: room was not created");
            }

            //make maproom
            MapRoom mapRoom = new MapRoom
            {
                MapFloorId = createRoomDto.MapFloorId,
                RoomId = room.Id,
                XCoordinate = createRoomDto.XCoordinate,
                YCoordinate = createRoomDto.YCoordinate,
                Width = createRoomDto.Width,
                Height = createRoomDto.Height,
                RbgColour = createRoomDto.RgbColour
            };

            mapRoom = _mapService.CreateRoom(mapRoom);
            
            return Ok(mapRoom);
        }
    }
}