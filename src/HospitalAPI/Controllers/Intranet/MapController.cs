using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Buildings;
using HospitalLibrary.Buildings.Dtos;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Floors;
using HospitalLibrary.Floors.Dtos;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Dtos;
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
        private IFloorService _floorService;
        private IBuildingService _buildingService;
        public MapController(IMapService mapService, IRoomService roomService, IFloorService floorService, IBuildingService buildingService)
        {
            _mapService = mapService;
            _roomService = roomService;
            _floorService = floorService;
            _buildingService = buildingService;
        }

        [HttpGet]
        [Route("buildings")]
        public async Task<IActionResult> GetAllBuildings()
        {
            IEnumerable<MapBuilding> mapBuildings = await _mapService.GetAllBuildings();
            IEnumerable<MapBuildingDto> mapBuildingDtos = Array.Empty<MapBuildingDto>();
            foreach (MapBuilding mapBuilding in mapBuildings)
            {
                mapBuildingDtos = mapBuildingDtos.Append(new MapBuildingDto(mapBuilding));
            }
            return Ok(mapBuildingDtos);
        }

        [HttpGet]
        [Route("floors/{id}")]
        public async Task<IActionResult> GetFloorsByBuilding(int id)
        {
            IEnumerable<MapFloor> mapFloors = await _mapService.GetFloorsByBuilding(id);
            IEnumerable<MapFloorDto> mapFloorDtos = Array.Empty<MapFloorDto>();
            foreach (MapFloor mapFloor in mapFloors)
            {
                mapFloorDtos = mapFloorDtos.Append(new MapFloorDto(mapFloor));
            }
            return Ok(mapFloorDtos);
        }

        [HttpGet]
        [Route("rooms/{id}")]
        public async Task<IActionResult> GetRoomsByBuilding(int id)
        {
            IEnumerable<MapRoom> mapRooms = await _mapService.GetRoomsByFloor(id);
            IEnumerable<MapRoomDto> mapRoomDtos = Array.Empty<MapRoomDto>();
            foreach (MapRoom mapRoom in mapRooms)
            {
                mapRoomDtos = mapRoomDtos.Append(new MapRoomDto(mapRoom));
            }
            return Ok(mapRoomDtos);
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
            MapBuilding mapBuilding = _mapService.GetBuildingById(createFloorDto.BuildingId);

            Floor floor = createFloorDto.DtoToFloor(mapBuilding.BuildingId);
            floor = _floorService.Create(floor);
            if (floor == null) return Problem("Error: floor was not created");

            MapFloor mapFloor = createFloorDto.DtoToMapFloor();
            mapFloor.FloorId = floor.Id;

            mapFloor = _mapService.CreateFloor(mapFloor);
            return Ok(mapFloor);
        }

        [HttpPost]
        [Route("buildings")]
        public IActionResult CreateBuilding([FromBody] CreateBuildingDto createBuildingDto)
        {
            Building building = _buildingService.Create(createBuildingDto.DtoToBuilding());
            if (building == null) return Problem("Error: building was not created");

            MapBuilding mapBuilding = createBuildingDto.DtoToMapBuilding();
            mapBuilding.BuildingId = building.Id;

            mapBuilding = _mapService.CreateBuilding(mapBuilding);
            return Ok(mapBuilding);
        }
    }
}