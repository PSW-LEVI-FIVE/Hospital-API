using HospitalLibrary.Appointments;
using HospitalLibrary.Migrations;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms
{
    public class EquipmentReallocationService : IEquipmentReallocationService
    {

        private readonly IUnitOfWork _unitOfWork;

        public EquipmentReallocationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<List<TimeInterval>> GetPossibleInterval(int Starting_roomId, int Destination_roomId,DateTime date, TimeSpan duration)
        {
            List<TimeInterval> intervalsA =await GetIntevals(Starting_roomId,date, duration);
            List<TimeInterval> intervalsB =await GetIntevals(Destination_roomId, date, duration);

            List<TimeInterval> intervals = getShared(intervalsA, intervalsB, duration);
            return intervals;
           //
           //return intervalsA;      
        }
        private async Task<List<TimeInterval>> GetIntevals(int roomId, DateTime date, TimeSpan duration)
        {
             
                return await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(roomId, date);

            //IEnumerable<TimeInterval> roomTimeIntervals =
            //     (IEnumerable<TimeInterval>)_unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(roomId, date);
            // IEnumerable<TimeInterval> roomReallocationsIntervals =
            //     (IEnumerable<TimeInterval>)_unitOfWork.EquipmentReallocationRepository.GetAllRoomTakenInrevalsForDate(roomId, date);


            //  List<TimeInterval> mixedIntervals = (List<TimeInterval>)roomReallocationsIntervals.Concat(roomTimeIntervals).GetEnumerator();


            //  return GetAvailableIntervals(mixedIntervals, date, duration);
        }
        List<TimeInterval> GetAvailableIntervals(List<TimeInterval> takenIntervals, DateTime date, TimeSpan duration)
        {
            List<TimeInterval> available = new List<TimeInterval>();
            DateTime b = date.AddHours(8);

            foreach (TimeInterval interval in takenIntervals)
            {
                if (b.Subtract(interval.Start) >= duration)
                    available.Add(new TimeInterval(date, interval.Start));
                b = interval.End;
            }
            if (b.Subtract(date.AddHours(10)) >= duration)
                available.Add(new TimeInterval(b, date));
            return available;
        }
        private List<TimeInterval> getShared(List<TimeInterval> intervalsA, List<TimeInterval> intervalsB ,TimeSpan duration)
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
                        shared.Add(a);
                    }
                }
            }
            return shared;
        }

        private  TimeInterval makeShared(TimeInterval interval, TimeInterval interval2, TimeSpan duration)
        {
            if (interval.Start <= interval2.Start || interval.End <= interval2.End || interval.End.Subtract(interval2.Start) >= duration)
            {
                return new TimeInterval(interval2.Start, interval.End);
            }
            if (interval.Start <= interval2.Start || interval.End >= interval2.End || interval2.End.Subtract(interval2.Start) >= duration)
            {
                return new TimeInterval(interval2.Start, interval.End);
            }
            if (interval.Start >= interval2.Start || interval.End <= interval2.End || interval.End.Subtract(interval.Start) >= duration)
            {
                return new TimeInterval(interval2.Start, interval.End);
            }
            if (interval.Start >= interval2.Start || interval.End >= interval2.End || interval2.End.Subtract(interval.Start) >= duration)
            {
                return new TimeInterval(interval2.Start, interval.End);
            }
            return null;

        }

    }
}
