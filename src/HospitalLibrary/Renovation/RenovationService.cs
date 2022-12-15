using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ceTe.DynamicPDF.PageElements.Forms;
using HospitalLibrary.Appointments;
using HospitalLibrary.Renovation.Interface;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Renovation
{
    public class RenovationService: IRenovationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RenovationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public async Task<Model.Renovation> Create(Model.Renovation renovation)
        {
            //validate renovation
            _unitOfWork.RenovationRepository.Add(renovation);
            _unitOfWork.RenovationRepository.Save();

            return renovation;
        }

        public async Task<List<TimeInterval>> GenerateTimeSlots(TimeInterval timeInterval,int duration,int roomid)
        {
            List<TimeInterval> slots = new List<TimeInterval>();
            var numberOfDaysInInterval = (timeInterval.End.Date.Subtract(timeInterval.Start.Date)).Days;

            for (int i = 0; i <= numberOfDaysInInterval - duration; i++)
            {
                var day = timeInterval.Start.Date.AddDays(i);
                var latest =await GetLatest(day,roomid);
                var empty = await IsRoomFreeForDay(timeInterval.Start.AddDays(i), roomid);
                if (!empty )
                {
                    if (latest == null) continue;
                    var freeRoom = await IsRoomFreeForDays(latest.End, duration - 1, roomid);
                    if  (duration<=1 || !freeRoom) continue;
                    
                    var earliest = await GetEarliest(latest.End.AddDays(duration),roomid);
                   
                    if(earliest==null) {slots.Add(new TimeInterval(latest.End, latest.End.AddDays(duration)));
                        continue;
                    }

                    var a = earliest.Start.CompareTo(latest.End.AddDays(duration));
                    if (a > 0)
                        slots.Add(new TimeInterval(latest.End, latest.End.AddDays(duration)));
                }
                else
                {
                    var freeRoom = await IsRoomFreeForDays(day, duration - 1, roomid);

                    if (duration <= 1 || !freeRoom) continue;
                    
                    var earliest = await GetEarliest(day.AddDays(duration),roomid);

                    if (earliest != null) slots.Add(new TimeInterval(earliest.Start.AddDays(-2),earliest.Start));

                   // var a=BreakInto30MinuteSlots(day.Date.AddHours(20), day.Date.AddDays(duration).AddHours(20));
                   // slots.AddRange(a);
                }
            }
            return slots;
        }

        public List<TimeInterval> BreakInto30MinuteSlots(DateTime startDate, DateTime EndDate)
        {   
            var list = new List<TimeInterval>();
            var repeats=(EndDate- EndDate.Date.AddHours(8)).Hours/0.5;
            for (int i = 0; i < repeats; i++)
            {
                list.Add(new TimeInterval(new TimeInterval(startDate.AddMinutes(-30*i), EndDate.AddMinutes(-30*i))));
            }

            return list;
        }
        public async Task<Boolean> IsRoomFreeForDays(DateTime day,int numberOfDays,int roomid)
        {
            for(int i = 1; i < numberOfDays; i++)
                if( !await IsRoomFreeForDay(day.AddDays(i),roomid)) return false;
            return true;
        }
        public async Task<Boolean> IsRoomFreeForDay(DateTime day,int roomId)
        {
            var renovations = await _unitOfWork.RenovationRepository.GetFirstPendingForDay(day,roomId);
            var reallocations = await _unitOfWork.EquipmentReallocationRepository.GetAllPendingForDateAndRoom(day,roomId);
            var appointments = await _unitOfWork.AppointmentRepository.GetAllPendingForDate(day, roomId);
            var activeReno = await _unitOfWork.RenovationRepository.GetActiveRenovationForDay(day, roomId);

            if (renovations == null &&activeReno==null && reallocations.Count==0 && appointments.Count==0)
                return  true;
            return false;
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
        public async Task<TimeInterval> GetEarliest(DateTime date,int roomId)
        {
            var earliest = await GetAllEarliestForDate(date, roomId);
            if (earliest.Count == 0) return null;
            earliest.OrderBy(x => x.End);
            return earliest[0];
        }

        private async Task<List<TimeInterval>> GetAllEarliestForDate(DateTime date,int roomId)
        {
            List<TimeInterval> latest = new List<TimeInterval>();
            var latestReallocation = await _unitOfWork.EquipmentReallocationRepository.GetFirstPendingForDay(date, roomId);
            var latestRenovation = await _unitOfWork.RenovationRepository.GetFirstPendingForDay(date, roomId);
            var latestAppointment = await _unitOfWork.AppointmentRepository.GetFirstForDate(date, roomId);
            if (latestReallocation != null)
                latest.Add(new TimeInterval(latestReallocation.StartAt, latestReallocation.EndAt));
            if (latestRenovation != null)
                latest.Add(latestRenovation);
            if (latestAppointment != null)
                latest.Add(latestAppointment);

            return latest;
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
