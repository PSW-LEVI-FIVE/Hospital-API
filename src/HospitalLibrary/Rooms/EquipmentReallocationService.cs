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

        public EquipmentReallocationService(IUnitOfWork unitOfWork,ITimeIntervalValidationService intervalValidation)
        {
            _unitOfWork = unitOfWork;
            _intervalValidation = intervalValidation;
        }

        public async Task<EquipmentReallocation> Create(EquipmentReallocation equipmentReallocation)
        {
            await _intervalValidation.ValidateReallocation(equipmentReallocation);
            _unitOfWork.EquipmentReallocationRepository.Add(equipmentReallocation);
            _unitOfWork.EquipmentReallocationRepository.Save();

            return equipmentReallocation;
        }
        public Task Delete(int id)
        {
            return null;
        }

        public Task<IEnumerable<EquipmentReallocation>> GetAll()
        {
            return null;
        }

        public Task<IEnumerable<EquipmentReallocation>> GetByRoom(int roomId)
        {
            return null;

        }
        public async Task<List<EquipmentReallocation>> getAllPending()
        {
            return await _unitOfWork.EquipmentReallocationRepository.GetAllPending();
        }
        public async Task<List<EquipmentReallocation>> getAllPendingForToday()
        {
            return await _unitOfWork.EquipmentReallocationRepository.GetAllPendingForToday();
        }

        public async Task<List<Model.RoomEquipment>> getEquipmentByRoom(int roomId) 
        {
            return await _unitOfWork.RoomEquipmentRepository.GetEquipmentByRoom(roomId);
        }
        public async Task<int> getReservedEquipment(int equipmentId) 
        {
            return _unitOfWork.RoomEquipmentRepository.GetNumberOfUsedEquipment(equipmentId);
        }

        public async Task<List<TimeInterval>> GetPossibleInterval(int Starting_roomId, int Destination_roomId,DateTime date, TimeSpan duration)
        {
            List<TimeInterval> intervalsA =await GetTakenIntevals(Starting_roomId,date);
            intervalsA= await GetAvailableIntervals(intervalsA, date, duration);

            List<TimeInterval> intervalsB =await GetTakenIntevals(Destination_roomId, date);
            intervalsB = await GetAvailableIntervals(intervalsB, date, duration);

            List<TimeInterval> intervals = await getShared(intervalsA, intervalsB, duration);
            

            return await smallerIntervals(intervals, duration);
     
        }

        private async Task<List<TimeInterval>> smallerIntervals(List<TimeInterval> intervals, TimeSpan duration)
        {
            List<TimeInterval> smalIntervals = new List<TimeInterval>();

            foreach (var interval in intervals)
            {
                smalIntervals.AddRange(snipInterval(interval, duration));
            }
            return smalIntervals;
        }

        
        private List<TimeInterval> snipInterval(TimeInterval interval, TimeSpan duration)
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

        public async Task<List<TimeInterval>> GetTakenIntevals(int roomId, DateTime date)
        {
            List<TimeInterval> roomTimeIntervals =
                         await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(roomId, date);
            List<TimeInterval> roomReallocationsIntervals =
                 await _unitOfWork.EquipmentReallocationRepository.GetAllRoomTakenInrevalsForDate(roomId, date);


            List<TimeInterval> mixedIntervals = roomReallocationsIntervals.Concat(roomTimeIntervals).ToList();
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

        private async Task<List<TimeInterval>> getShared(List<TimeInterval> intervalsA, List<TimeInterval> intervalsB ,TimeSpan duration)
        {
            List<TimeInterval> shared = new List<TimeInterval>(); 
            foreach(TimeInterval interval in intervalsA)
            {
                foreach(TimeInterval interval2 in intervalsB)
                {
                    if (interval.IsOverlaping(interval2)) 
                    {
                       var a = makeShared(interval, interval2, duration);
                        if (a != null)
                        {
                            interval.Start = a.End;
                            shared.Add(a);
                        }
                    }
                }
            }
            return shared;
        }

        private  TimeInterval makeShared(TimeInterval interval, TimeInterval interval2, TimeSpan duration)
        {
            if (interval.Start <= interval2.Start && interval.End <= interval2.End && interval.End.Subtract(interval2.Start) >= duration)
            {
                return new TimeInterval(interval2.Start, interval.End);
            }
            if (interval.Start <= interval2.Start && interval.End >= interval2.End && interval2.End.Subtract(interval2.Start) >= duration)
            {
                return new TimeInterval(interval2.Start, interval.End);
            }
            if (interval.Start >= interval2.Start && interval.End <= interval2.End && interval.End.Subtract(interval.Start) >= duration)
            {
                return new TimeInterval(interval2.Start, interval.End);
            }
            if (interval.Start >= interval2.Start && interval.End >= interval2.End && interval2.End.Subtract(interval.Start) >= duration)
            {
                return new TimeInterval(interval2.Start, interval.End);
            }
            return null;

        }

        public async Task initiate(EquipmentReallocation reallocation)
        {
            await moveOutOfStartingRoom(reallocation.StartingRoomId,reallocation.EquipmentId, reallocation.amount);
            await moveIntoDestinationRoom(reallocation.DestinationRoomId, reallocation.EquipmentId, reallocation.amount);

           
             reallocation.state = ReallocationState.FINISHED;
             _unitOfWork.EquipmentReallocationRepository.Update(reallocation);
            _unitOfWork.EquipmentReallocationRepository.Save();
        }

        private async Task moveIntoDestinationRoom(int destinationRoomId, int equipmentId, int amount)
        {
            var equipment=_unitOfWork.RoomEquipmentRepository.GetOne(equipmentId);
            RoomEquipment realEq =await _unitOfWork.RoomEquipmentRepository.GetEquipmentByRoomAndName(destinationRoomId, equipment.Name);
            if(realEq == null)
            {
                
                _unitOfWork.RoomEquipmentRepository.Add(new RoomEquipment(15,amount, equipment.Name, destinationRoomId));
                _unitOfWork.RoomEquipmentRepository.Save();

            }
            else
            {
                realEq.Quantity += amount;
                _unitOfWork.RoomEquipmentRepository.Update(realEq);
                _unitOfWork.RoomEquipmentRepository.Save();
            }

        }

        private async Task moveOutOfStartingRoom(int startingRoomId, int equipmentId, int amount)
        {
            var equipment = _unitOfWork.RoomEquipmentRepository.GetOne(equipmentId);
            equipment.Quantity -= amount;
            _unitOfWork.RoomEquipmentRepository.Update(equipment);
            _unitOfWork.RoomEquipmentRepository.Save();
        }
    }
}
