using HospitalLibrary.Appointments;
using HospitalLibrary.Migrations;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.DTOs;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms
{
    public class EquipmentReallocationService : IEquipmentReallocationService
    {
        private readonly ITimeIntervalValidationService _intervalValidation;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomEquipmentService _equipmentService;

        public EquipmentReallocationService(IUnitOfWork unitOfWork,ITimeIntervalValidationService intervalValidation, IRoomEquipmentService equipmentService)
        {
            _unitOfWork = unitOfWork;
            _equipmentService = equipmentService;
            _intervalValidation = intervalValidation;
        }
        
        public async Task<EquipmentReallocation> Create(EquipmentReallocation equipmentReallocation)
        {
            await _intervalValidation.ValidateReallocation(equipmentReallocation);
            _unitOfWork.EquipmentReallocationRepository.Add(equipmentReallocation);
            _unitOfWork.EquipmentReallocationRepository.Save();

            return equipmentReallocation;
        }
        private void Update(EquipmentReallocation reallocation)
        {
            _unitOfWork.EquipmentReallocationRepository.Update(reallocation);
            _unitOfWork.EquipmentReallocationRepository.Save();
        }
        public async Task Delete(int id)
        { 
             _unitOfWork.EquipmentReallocationRepository.Delete(_unitOfWork.EquipmentReallocationRepository.GetOne(id));
        }

        public async Task<IEnumerable<EquipmentReallocation>> GetAll()
        {
           return await _unitOfWork.EquipmentReallocationRepository.GetAll();
        }

      
        public async Task<List<EquipmentReallocation>> GetAllPending()
        {
            return await _unitOfWork.EquipmentReallocationRepository.GetAllPending();
        }
        public async Task<List<EquipmentReallocation>> GetAllPendingForToday()
        {
            return await _unitOfWork.EquipmentReallocationRepository.GetAllPendingForToday();
        }

        public async Task<List<Model.RoomEquipment>> GetEquipmentByRoom(int roomId) 
        {
            return await _unitOfWork.RoomEquipmentRepository.GetEquipmentByRoom(roomId);
        }
        public async Task<int> GetReservedEquipment(int equipmentId) 
        {
            return _unitOfWork.RoomEquipmentRepository.GetNumberOfUsedEquipment(equipmentId);
        }

        public async Task<List<TimeInterval>> GetPossibleInterval(int Starting_roomId, int Destination_roomId,DateTime date, TimeSpan duration)
        {
            List<TimeInterval> intervalsA =await GetTakenIntervals(Starting_roomId,date);
            intervalsA= await GetAvailableIntervals(intervalsA, date, duration);

            List<TimeInterval> intervalsB =await GetTakenIntervals(Destination_roomId, date);
            intervalsB = await GetAvailableIntervals(intervalsB, date, duration);

            List<TimeInterval> intervals = await GetShared(intervalsA, intervalsB, duration);
            

            return await SmallerIntervals(intervals, duration);
     
        }

        private async Task<List<TimeInterval>> SmallerIntervals(List<TimeInterval> intervals, TimeSpan duration)
        {
            List<TimeInterval> smalIntervals = new List<TimeInterval>();

            foreach (var interval in intervals)
            {
                smalIntervals.AddRange(SnipInterval(interval, duration));
            }
            return smalIntervals;
        }

        
        private List<TimeInterval> SnipInterval(TimeInterval interval, TimeSpan duration)
        {
            List<TimeInterval> snippedInterval = new List<TimeInterval>();
            var start=interval.Start;
            int reps = interval.NumberOfTimespans(duration);
            for (int i = 0; i < reps; i++)
            {
                snippedInterval.Add(new TimeInterval(start,start=start + duration));
            }
            return snippedInterval;
        }

        public async Task<List<TimeInterval>> GetTakenIntervals(int roomId, DateTime date)
        {
            List<TimeInterval> roomTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(roomId, date);
            List<TimeInterval> roomReallocationIntervals =
                await _unitOfWork.EquipmentReallocationRepository.GetAllRoomTakenInrevalsForDate(roomId, date);


            List<TimeInterval> mixedIntervals = roomReallocationIntervals.Concat(roomTimeIntervals).ToList();
            return mixedIntervals = mixedIntervals.OrderBy(x => x.Start).ToList();
        }

        private async Task<List<TimeInterval>> GetAvailableIntervals(List<TimeInterval> takenIntervals, DateTime date, TimeSpan duration)
        {
            List<TimeInterval> available = new List<TimeInterval>();
            DateTime b = date.Date.AddHours(8);

            foreach (TimeInterval interval in takenIntervals)
            {
                if (interval.Start.Subtract(b) >= duration)
                    available.Add(new TimeInterval(b, interval.Start));
                b = interval.End;
            }
            date=date.Date.AddHours(21);
            if (date.Subtract(b) >= duration)
                available.Add(new TimeInterval(b,date));
            return available;
        }

        private async Task<List<TimeInterval>> GetShared(List<TimeInterval> intervalsA, List<TimeInterval> intervalsB ,TimeSpan duration)
        {
            List<TimeInterval> shared = new List<TimeInterval>(); 
            foreach(var interval in intervalsA)
            {
                foreach(var interval2 in intervalsB)
                {
                    if (interval.IsOverlaping(interval2)) continue;
                    var sharedInterval =MakeShared(interval, interval2, duration);
                    interval.Start = sharedInterval.End;
                    shared.Add(sharedInterval);
                }
            }
            return shared;
        }

        private static TimeInterval MakeShared(TimeInterval interval, TimeInterval interval2, TimeSpan duration)
        {
            if (interval.Start <= interval2.Start && interval.End.Subtract(interval2.Start) >= duration)
            {
                return new TimeInterval(interval2.Start, interval.End);
            }
            if (interval.Start >= interval2.Start && interval.End.Subtract(interval.Start) >= duration)
            {
                return new TimeInterval(interval.Start, interval2.End);
            }
            
            return null;
        }

        public async Task InitiateReallocation(EquipmentReallocation reallocation)
        {
            await MoveOutOfStartingRoom(reallocation.StartingRoomId,reallocation.EquipmentId, reallocation.amount);
            await MoveIntoDestinationRoom(reallocation.DestinationRoomId, reallocation.EquipmentId, reallocation.amount);

           
             reallocation.state = ReallocationState.FINISHED;
             Update(reallocation);
        }

        private async Task MoveIntoDestinationRoom(int destinationRoomId, int equipmentId, int amount)
        {
            var equipment=_unitOfWork.RoomEquipmentRepository.GetOne(equipmentId);
            RoomEquipment realEq =await _unitOfWork.RoomEquipmentRepository.GetEquipmentByRoomAndName(destinationRoomId, equipment.Name);
            if(realEq == null)
            {
                _equipmentService.CreateEquipment(destinationRoomId, amount, equipment);
            }
            else
            {
                realEq.Quantity += amount;
                _equipmentService.UpdateEquipment(realEq);
            }
            
        }

        private async Task MoveOutOfStartingRoom(int startingRoomId, int equipmentId, int amount)
        {
            var equipment = _unitOfWork.RoomEquipmentRepository.GetOne(equipmentId);
            equipment.Quantity -= amount;
            _equipmentService.UpdateEquipment(equipment);
        }
        
    }
}
