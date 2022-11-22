using HospitalLibrary.Appointments;
using HospitalLibrary.Migrations;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Interfaces;
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

            List<TimeInterval> intervals = await getShared(intervalsA, intervalsB, duration);
            //return intervals;

            return await smallerIntervals(intervals, duration);
           //
           //return intervalsA;      
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

        private async Task<List<TimeInterval>> GetIntevals(int roomId, DateTime date, TimeSpan duration)
        {
             
           //     return await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(roomId, date);

            List<TimeInterval> roomTimeIntervals =
                         await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(roomId, date);
            List<TimeInterval> roomReallocationsIntervals =
                 await _unitOfWork.EquipmentReallocationRepository.GetAllRoomTakenInrevalsForDate(roomId, date);


            List<TimeInterval> mixedIntervals = roomReallocationsIntervals.Concat(roomTimeIntervals).ToList();
            mixedIntervals = mixedIntervals.OrderBy(x => x.Start).ToList();

            return await GetAvailableIntervals(mixedIntervals, date, duration);
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

    }
}
