using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Map;
using HospitalLibrary.Renovations.Interface;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model.ValueObjects;
using OpenQA.Selenium.DevTools.V106.HeadlessExperimental;

namespace HospitalLibrary.Renovations
{
  public class RenovationService : IRenovationService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITimeIntervalValidationService _intervalValidation;
    private readonly IRoomEquipmentService _equipmentService;
    private readonly IRoomService _roomService;
    private IRenovationValidator _renovationValidator;

    public RenovationService(IUnitOfWork unitOfWork, ITimeIntervalValidationService intervalValidation, IRoomEquipmentService equipmentService,
                            IRoomService roomService,IRenovationValidator renovationValidator)
    {
      _unitOfWork = unitOfWork;
      _intervalValidation = intervalValidation;
      _equipmentService = equipmentService;
      _roomService = roomService;
      _renovationValidator = renovationValidator;

    }

    public async Task<Model.Renovation> Create(Model.Renovation renovation)
    {
      //validate renovation
      await _intervalValidation.ValidateRenovation(renovation);

      _unitOfWork.RenovationRepository.Add(renovation);
      _unitOfWork.RenovationRepository.Save();

      return renovation;
    }

    public async Task<List<TimeInterval>> GenerateCleanerTimeSlots(TimeInterval timeInterval, int duration,
        int roomId)
    {
      List<TimeInterval> slots = new List<TimeInterval>();
      var schedule = await GetRoomScheduleForRange(timeInterval, roomId);
      DateTime previous = timeInterval.Start;
      foreach (var current in schedule)
      {

        if (current.Start.Subtract(previous).CompareTo(new TimeSpan(duration, 0, 0, 0)) < 0)
        {
          previous = current.End;
          continue;

        }

        var actual = BreakIntoDurationLengthSlots(previous, current.Start, duration);
        slots.AddRange(actual);
        previous = current.End;
      }

      if (timeInterval.End.Subtract(previous).CompareTo(new TimeSpan(duration, 0, 0, 0)) >= 0)
        slots.AddRange(BreakIntoDurationLengthSlots(previous, timeInterval.End, duration));

      return slots;
    }

    private async Task<List<TimeInterval>> GetRoomScheduleForRange(TimeInterval interval, int roomId)
    {
      var reallocation = await _unitOfWork.EquipmentReallocationRepository.GetAllPendingForRoomInTimeInterval(roomId, interval);
      List<TimeInterval> latest = reallocation.Select(equipmentReallocation => equipmentReallocation.GetInterval()).ToList();


      var appointment = await _unitOfWork.AppointmentRepository.GetAllPendingForRange(interval, roomId);
      latest.AddRange(appointment.Select(timeInterval => new TimeInterval(timeInterval.Start, timeInterval.End)));


      var renovations = await _unitOfWork.RenovationRepository.GetAllPendingForRoomInRange(interval, roomId);
      latest.AddRange(renovations.Select(r => r.GetInterval()));


      return latest.OrderBy(a => a.Start).ToList();
    }

    public List<TimeInterval> BreakIntoDurationLengthSlots(DateTime startDate, DateTime endDate, int duration)
    {
      var list = new List<TimeInterval>();
      var repeats = endDate.Subtract(startDate).Hours + endDate.Subtract(startDate).Days * 24;
      repeats = repeats / (24 * duration);

      for (var i = 0; i < repeats; i++)
      {
        list.Add(new TimeInterval(startDate.AddDays(i), startDate.AddDays(i + 1)));
      }

      return list;
    }


    public async Task<Model.Renovation> GetOne(int id)
    {
      var a = await _unitOfWork.RenovationRepository.GetAllPending();
      return a[0];
    }

    public async Task ExecuteRenovation(Model.Renovation renovation)
    {
      switch (renovation.Type)
      {
        case RenovationType.SPLIT:
          {
            SplitRoom(renovation.roomName, _unitOfWork.RoomRepository.GetOne(renovation.MainRoomId));

            renovation.State = RenovationState.FINISHED;
            Update(renovation);
            break;
          }
        case RenovationType.MERGE:
          if (renovation.SecondaryRoomIds == null) throw new Exception("secondary room can't be null");

          IEnumerable<Room> secondaryRooms = Array.Empty<Room>();

          foreach (int id in renovation.GetSecondaryIds())
          { 
            secondaryRooms = secondaryRooms.Append(_roomService.GetOne(id));
          }

          await MergeRooms(_roomService.GetOne(renovation.MainRoomId),
              secondaryRooms.ToList());

          renovation.State = RenovationState.FINISHED;
          Update(renovation);

          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public async Task<List<Model.Renovation>> GetAllPendingForRoom(int roomId)
    {
      return await _unitOfWork.RenovationRepository.GetAllPendingForRoom(roomId);
    }

    public Model.Renovation CancelRenovation(int renovationId)
    {
      Model.Renovation renovation = _unitOfWork.RenovationRepository.GetOne(renovationId);
      _renovationValidator.ThrowIfLessThan24hours(renovation);
      renovation.State = RenovationState.CANCELED;
      _unitOfWork.RenovationRepository.Update(renovation);
      _unitOfWork.RenovationRepository.Save();
      return renovation;
    }

    private async Task MergeRooms(Room mainRoom, List<Room> secondaryRooms)
    {

      foreach (var room in secondaryRooms)
      {
        float newArea = mainRoom.Area.Measure + room.Area.Measure;
        mainRoom.Area = new Area(newArea);
      }

      _roomService.Update(mainRoom);

      await TransferAllEquipment(mainRoom, secondaryRooms);
      await TransferAllAppointments(mainRoom, secondaryRooms);
      await DeleteAllReallocation(secondaryRooms, mainRoom);
      TransferCoordinates(mainRoom, secondaryRooms);

      DeleteSecondaryRooms(secondaryRooms);
    }

    private void TransferCoordinates(Room mainRoom, List<Room> secondaryRooms)
    {
      IEnumerable<Coordinates> secondaryCoordinates = Array.Empty<Coordinates>();
      foreach (var room in secondaryRooms)
      {
        secondaryCoordinates = secondaryCoordinates.Append(_unitOfWork.MapRoomRepository.GetOne(room.Id).Coordinates);
      }

      MapRoom mainMapRoom = _unitOfWork.MapRoomRepository.GetOne(mainRoom.Id);
      mainMapRoom.SecondaryCoordinatesList = new CoordinatesList(secondaryCoordinates);
      
      _unitOfWork.MapRoomRepository.Save();
    }

    private void DeleteSecondaryRooms(List<Room> secondaryRooms)
    {
      foreach (var room in secondaryRooms)
      {
        _unitOfWork.RoomRepository.Delete(room);
        _unitOfWork.MapRoomRepository.Delete(_unitOfWork.MapRoomRepository.GetOne(room.Id));
      }
      
      _unitOfWork.RoomRepository.Save();
      _unitOfWork.MapRoomRepository.Save();
    }
    
    private void SplitRoom(string roomNumber, Room mainRoom)
    {
      var id = _unitOfWork.RoomRepository.GetMaxId();
      var room2 = new Room(id + 1, roomNumber, new Area(mainRoom.Area.Measure / 2),
          mainRoom.FloorId, RoomType.OPERATION_ROOM);
      
      //mainRoom.Area = new Area(mainRoom.Area.Measure / 2);
      float newArea = mainRoom.Area.Measure / 2;
      mainRoom.UpdateArea(new Area(newArea));

      _roomService.Create(room2);
      _roomService.Update(mainRoom);
    }

    private async Task DeleteAllReallocation(List<Room> renovationSecondaryRooms, Room mainRoom)
    {
      List<EquipmentReallocation> equipmentReallocations = new List<EquipmentReallocation>();

      foreach (var room in renovationSecondaryRooms.ToList())
      {
        equipmentReallocations.AddRange(await _unitOfWork.EquipmentReallocationRepository.GetAllForRoom(room.Id));
      }

      var appointmentsMain =
          await GetRoomScheduleForRange(new TimeInterval(DateTime.Now.AddYears(-2), DateTime.Now.AddYears(50)),
              mainRoom.Id);
      if (!equipmentReallocations.Any()) return;

      foreach (var eq in equipmentReallocations)
      {
        if (!IsMergeable(appointmentsMain, new TimeInterval(eq.StartAt, eq.EndAt)) &&
            eq.state != ReallocationState.PENDING)
        {
          _unitOfWork.EquipmentReallocationRepository.Delete(eq);
          _unitOfWork.EquipmentReallocationRepository.Save();
        }

        if (renovationSecondaryRooms.FindIndex(room => room.Id == eq.StartingRoomId) >= 0)
          eq.StartingRoomId = mainRoom.Id;
        else eq.DestinationRoomId = mainRoom.Id;

        _unitOfWork.EquipmentReallocationRepository.Update(eq);
        _unitOfWork.EquipmentReallocationRepository.Save();
      }
    }
    private async Task TransferAllAppointments(Room mainRoom, IEnumerable<Room> secondaryRooms)
    {

      List<Appointment> appointmentsSecondary = new List<Appointment>();

      foreach (var room in secondaryRooms)
      {
        appointmentsSecondary.AddRange(await _unitOfWork.AppointmentRepository.GetAllForRoom(room.Id));
      }
      
      var appointmentsMain =
          await GetRoomScheduleForRange(new TimeInterval(DateTime.Now.AddYears(-2), DateTime.Now.AddYears(50)),
              mainRoom.Id);

      if (!appointmentsSecondary.Any()) return;
      foreach (var appointment in appointmentsSecondary)
      {
        if (!IsMergeable(appointmentsMain, new TimeInterval(appointment.StartAt, appointment.EndAt)) && appointment.State != AppointmentState.PENDING)
        {
          _unitOfWork.AppointmentRepository.Delete(appointment);
          _unitOfWork.AppointmentRepository.Save();
        }

        appointment.RoomId = mainRoom.Id;
        _unitOfWork.AppointmentRepository.Update(appointment);
        _unitOfWork.AppointmentRepository.Save();
      }
    }
    private static bool IsMergeable(List<TimeInterval> appointmentsMain, TimeInterval slot)
    {
      if (appointmentsMain.Count == 0) return true;
      return appointmentsMain.All(appointmentSec => !new TimeInterval(appointmentSec.Start, appointmentSec.End)
          .IsOverlaping(new TimeInterval(slot.Start, slot.End)));
    }
    private async Task TransferAllEquipment(Room mainRoom, IEnumerable<Room> secondaryRooms)
    {
      List<RoomEquipment> secondaryRoomEq = new List<RoomEquipment>();

      foreach (var room in secondaryRooms)
      {
        secondaryRoomEq.AddRange(await _unitOfWork.RoomEquipmentRepository.GetEquipmentByRoom(room.Id));
      }
      
      var primaryRoomEq = await _unitOfWork.RoomEquipmentRepository.GetEquipmentByRoom(mainRoom.Id);

      foreach (var eq in secondaryRoomEq)
      {
        var item = primaryRoomEq.Find(item => eq.Name == item.Name);
        if (item != null)
        {
          item.Quantity += eq.Quantity;
          _equipmentService.UpdateEquipment(item);

          _equipmentService.DeleteEquipment(eq);
        }
        else
        {
          eq.RoomId = mainRoom.Id;
          _equipmentService.UpdateEquipment(eq);
        }
      }
    }


    public void Update(Model.Renovation renovation)
    {
      //validate renovation
      _unitOfWork.RenovationRepository.Update(renovation);
      _unitOfWork.RenovationRepository.Save();
    }
    public Task Delete(int id)
    {
      _unitOfWork.RenovationRepository.Delete(_unitOfWork.RenovationRepository.GetOne(id));
      _unitOfWork.RenovationRepository.Save();
      return Task.CompletedTask;
    }

    public async Task<List<Model.Renovation>> GetAllPending()
    {
      return await _unitOfWork.RenovationRepository.GetAllPending();
    }
  }

}
