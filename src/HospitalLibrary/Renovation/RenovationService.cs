using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ceTe.DynamicPDF.PageElements.Forms;
using HospitalLibrary.Appointments;
using HospitalLibrary.Renovation.Interface;
using HospitalLibrary.Renovation.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Renovation
{
    public class RenovationService: IRenovationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeIntervalValidationService _intervalValidation;

        public RenovationService(IUnitOfWork unitOfWork, ITimeIntervalValidationService intervalValidation)
        {
            _unitOfWork=unitOfWork;
            _intervalValidation = intervalValidation;

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
            var schedule =await GetRoomScheduleForRange(timeInterval, roomId);
            DateTime previous = timeInterval.Start;
                ;
            foreach (var current in schedule)
            {

                if (current.Start.Subtract(previous).CompareTo(new TimeSpan(duration,0,0,0))<0)
                {
                    previous = current.End; 
                    continue;

                }

                var actual= BreakIntoDurationLengthSlots(previous, current.Start,duration);
                slots.AddRange(actual);
                previous = current.End;
            }

            if(timeInterval.End.Subtract(previous).CompareTo(new TimeSpan(duration, 0, 0, 0)) >= 0) 
                slots.AddRange(BreakIntoDurationLengthSlots(previous,timeInterval.End,duration));

            return slots;
        }

        private async Task<List<TimeInterval>> GetRoomScheduleForRange(TimeInterval interval, int roomId)
        {
            List<TimeInterval> latest = new List<TimeInterval>();
            var Realocations = await _unitOfWork.EquipmentReallocationRepository.GetAllPendingForRoomInTimeInterval(roomId,interval);
            foreach (var realocation in Realocations)
                latest.Add(realocation.GetInterval());
            

            var Appointment = await _unitOfWork.AppointmentRepository.GetAllPendingForRange(interval,roomId);
            foreach (var appointment in Appointment)
                latest.Add(new TimeInterval(appointment.Start,appointment.End));
            

            var Renovations = await _unitOfWork.RenovationRepository.GetAllPendingForRoomInRange(interval, roomId);
            foreach (var r in Renovations)
                latest.Add(r.GetInterval());
            

            return latest.OrderBy(a => a.Start).ToList();
        }

        public List<TimeInterval> BreakIntoDurationLengthSlots(DateTime startDate, DateTime EndDate, int duration)
        {   
            var list = new List<TimeInterval>();
            var repeats=EndDate.Subtract(startDate).Hours + EndDate.Subtract(startDate).Days*24;
            repeats=repeats/(24*duration);

            for (var i = 0; i < repeats; i++)
            {
                list.Add(new TimeInterval(startDate.AddDays(i), startDate.AddDays(i+1)));
            }

            return list;
        }
       
        public async Task<TimeInterval> GetLatest(DateTime date,int roomId)
        {
            var latest = await GetAllLatestForDate(date,roomId);
            if (latest.Count == 0) return null;
            latest.OrderByDescending(x => x.End);
            return latest[0];
        }
        private async Task<List<TimeInterval>> GetAllLatestForDate(DateTime date,int roomId)
        {
            List<TimeInterval> latest = new List<TimeInterval>();
            var latestReallocation = await _unitOfWork.EquipmentReallocationRepository.GetLastPendingForDay(date, roomId);
            var latestRenovation = await _unitOfWork.RenovationRepository.GetLastPendingForDay(date, roomId);
            var latestAppointment = await _unitOfWork.AppointmentRepository.GetLastForDate(date, roomId);
            if (latestReallocation != null)
                latest.Add(new TimeInterval(latestReallocation.StartAt, latestReallocation.EndAt));
            if (latestRenovation != null)
                latest.Add(latestRenovation);
            if (latestAppointment != null)
                latest.Add(latestAppointment);

            return latest;
        }

        public async Task<Model.Renovation> GetOne(int id)
        {
            var a= await _unitOfWork.RenovationRepository.GetAllPending();
            return a[0];
        }

        public async Task initiateRenovation(Model.Renovation renovation)
        {
            switch (renovation.Type)
            {
                case RenovationType.SPLIT:
                {
                    var MainRoom = _unitOfWork.RoomRepository.GetOne(renovation.MainRoomId);
                    var roomNumber = renovation.roomName;
                    NewMethod(roomNumber, MainRoom);

                    renovation.State = RenovationState.FINISHED;
                    _unitOfWork.RenovationRepository.Update(renovation);
                    _unitOfWork.RenovationRepository.Save();
                    break;
                }
                case RenovationType.MERGE:
                    renovation.MainRoom = _unitOfWork.RoomRepository.GetOne(renovation.MainRoomId);
               
                    renovation.SecondaryRoom = _unitOfWork.RoomRepository.GetOne((int)renovation.SecondaryRoomId);

                    renovation.MainRoom.Area += renovation.SecondaryRoom.Area;

                    _unitOfWork.RoomRepository.Update(renovation.MainRoom);
                    _unitOfWork.RoomRepository.Save();

                    await TransferAllEquipment(renovation.MainRoom, renovation.SecondaryRoom);
                    await TransferAllAppointments(renovation.MainRoom,renovation.SecondaryRoom);
                    await DeleteAllReallocation(renovation.SecondaryRoom);

                    _unitOfWork.RoomRepository.Delete(renovation.SecondaryRoom);
                    _unitOfWork.RoomRepository.Save();

                    renovation.State = RenovationState.FINISHED;
                    renovation.SecondaryRoom=null;
                    renovation.SecondaryRoomId = null;
                    _unitOfWork.RenovationRepository.Update(renovation);
                    _unitOfWork.RenovationRepository.Save();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void NewMethod(string roomNumber, Room MainRoom)
        {
            var id = _unitOfWork.RoomRepository.GetMaxId();
            var room2 = new Room(id + 1, roomNumber, MainRoom.Area / 2,
                MainRoom.FloorId, RoomType.OPERATION_ROOM);

            MainRoom.Area = MainRoom.Area / 2;
            _unitOfWork.RoomRepository.Add(room2);
            _unitOfWork.RoomRepository.Update(MainRoom);
            _unitOfWork.RoomRepository.Save();
        }

        private async Task DeleteAllReallocation(Room renovationSecondaryRoom)
        {
            var equipmentReallos = await _unitOfWork.EquipmentReallocationRepository.GetAllForRoom(renovationSecondaryRoom.Id);
            foreach (var eq in equipmentReallos)
                _unitOfWork.EquipmentReallocationRepository.Delete(eq);
        }

        private async Task TransferAllEquipment(Room mainRoom, Room secondaryRoom)
        {
          var secondaryRoomEq= await _unitOfWork.RoomEquipmentRepository.GetEquipmentByRoom(secondaryRoom.Id);
          var primaryRoomEq = await _unitOfWork.RoomEquipmentRepository.GetEquipmentByRoom(mainRoom.Id);
            foreach(var eq in secondaryRoomEq)
            {
                if(IsInPrimaryRoom(primaryRoomEq, eq))
                {
                    var item=await _unitOfWork.RoomEquipmentRepository.GetEquipmentByRoomAndName(mainRoom.Id, eq.Name);
                    item.Quantity += eq.Quantity;
                    _unitOfWork.RoomEquipmentRepository.Update(item);
                    _unitOfWork.RoomEquipmentRepository.Save();

                    _unitOfWork.RoomEquipmentRepository.Delete(eq);
                    _unitOfWork.RoomEquipmentRepository.Save();


                }
                else
                {
                    eq.RoomId = mainRoom.Id;
                    _unitOfWork.RoomEquipmentRepository.Update(eq);
                    _unitOfWork.RoomEquipmentRepository.Save();
                }
            }
        }

        private bool IsInPrimaryRoom(List<RoomEquipment> primaryRoomEq, RoomEquipment eq)
        {
            foreach (var item in primaryRoomEq)
                if (eq.Name == item.Name)
                    return true;
            return false;
        }

        private async Task TransferAllAppointments(Room mainRoom,Room secondaryRoom)
        {
           var appointmentsSecondary=await _unitOfWork.AppointmentRepository.GetAllPendingForRoom(secondaryRoom.Id);
           var appointmentsMain = await _unitOfWork.AppointmentRepository.GetAllPendingForRoom(secondaryRoom.Id);

           if (!appointmentsSecondary.Any()) return;
           foreach (var appointment in appointmentsSecondary)
           {
               if (!IsMergable(appointmentsMain, appointment)) _unitOfWork.AppointmentRepository.Delete(appointment);
               appointment.RoomId = mainRoom.Id;
               _unitOfWork.AppointmentRepository.Update(appointment);
           }
        }

        private Boolean IsMergable(List<Appointment> appointmentsMain, Appointment appointment)
        {
            foreach (var appointmentSec in appointmentsMain)
            {
                if (new TimeInterval(appointmentSec.StartAt, appointmentSec.EndAt).IsOverlaping(
                        new TimeInterval(appointment.StartAt, appointment.EndAt)))
                {
                    return false;
                }
            }

            return true;
        }

        public void Update(Model.Renovation renovation)
        {
            //validate renovation
            _unitOfWork.RenovationRepository.Update(renovation);
            _unitOfWork.RenovationRepository.Save();
        }
        public async Task Delete(int id)
        {
            _unitOfWork.RenovationRepository.Delete(_unitOfWork.RenovationRepository.GetOne(id));
        }

        public async Task<List<Model.Renovation>> GetAllPending()
        {
            return await _unitOfWork.RenovationRepository.GetAllPending(); 
        }
    }
}
