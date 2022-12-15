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

        public async Task<List<TimeInterval>> GenerateTimeSlots(TimeInterval timeInterval,int duration)
        {
            List<TimeInterval> slots = new List<TimeInterval>();
            var numberOfDaysInInterval = (timeInterval.End.Date.Subtract(timeInterval.Start.Date)).Days;
            for (int i = 0; i <= numberOfDaysInInterval - duration; i++)
            {
                var day = timeInterval.Start.Date.AddDays(i);
                var latest =await GetLatest(day);

                if (latest!=null)
                {
                    if  (duration<=1 && ! await IsRoomFreeForDays(latest.Start, numberOfDaysInInterval - 1)) continue;
                        var earliest = await GetEarliest(latest.Start.AddDays(duration));

                    if (earliest != null && earliest.Start.Subtract(latest.Start.AddDays(duration)).Minutes>0) 
                        slots.Add(new TimeInterval(latest.Start,latest.Start.AddDays(duration)));
                }
                else
                {
                    var b = numberOfDaysInInterval - 1;
                    Console.WriteLine(await IsRoomFreeForDays(day, numberOfDaysInInterval - 1));
                    if (duration <= 1 || ! await IsRoomFreeForDays(day, numberOfDaysInInterval - 1)) continue;
                        var earliest = await GetEarliest(timeInterval.Start.AddDays(i+duration));

                    if (earliest != null) slots.Add(new TimeInterval(earliest.Start.AddDays(-2),earliest.Start));

                    var a=BreakInto30MinuteSlots(day.Date.AddHours(20), day.Date.AddDays(duration).AddHours(20));
                    slots.AddRange(a);
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
        public async Task<Boolean> IsRoomFreeForDays(DateTime day,int numberOfDays)
        {
            for(int i = 0; i < numberOfDays; i++)
                if( !await IsRoomFreeForDay(day.AddDays(i))) return false;
            return true;
        }
        public async Task<Boolean> IsRoomFreeForDay(DateTime day)
        {
            var renovations = await _unitOfWork.RenovationRepository.GetFirstPendingForDay(day);
            var reallocations = await _unitOfWork.EquipmentReallocationRepository.GetAllPendingForDate(day);
            var appointments = await _unitOfWork.AppointmentRepository.GetAllPendingForDate(day);
            var activeReno = await _unitOfWork.RenovationRepository.GetActiveRenovationForDay(day);

            if (renovations == null &&activeReno==null && reallocations.Count==0 && appointments.Count==0)
                //&& _unitOfWork.EquipmentReallocationRepository.GetAllPendingForDate(day) && _unitOfWork.AppointmentRepository.GetAllPendingForDate(day)==null)
                return  true;
            return false;
        }

        public async Task<TimeInterval> GetLatest(DateTime date)
        {
            var latest = await GetAllLatestForDate( date);
            if (latest.Count == 0) return null;
            latest.OrderByDescending(x => x.End);
            return latest[0];
        }
        private async Task<List<TimeInterval>> GetAllLatestForDate(DateTime date)
        {
            List<TimeInterval> latest = new List<TimeInterval>();
            var latestReallocation = await _unitOfWork.EquipmentReallocationRepository.GetLastPendingForDay(date);
            var latestRenovation = await _unitOfWork.RenovationRepository.GetLastPendingForDay(date);
            var latestAppointment = await _unitOfWork.AppointmentRepository.GetLastForDate(DateTime.Now);
            if (latestReallocation != null)
                latest.Add(new TimeInterval(latestReallocation.StartAt, latestReallocation.EndAt));
            if (latestRenovation != null)
                latest.Add(latestRenovation);
            if (latestAppointment != null)
                latest.Add(latestAppointment);

            return latest;
        }
        public async Task<TimeInterval> GetEarliest(DateTime date)
        {
            var earliest = await GetAllEarliestForDate(date);
            if (earliest.Count == 0) return null;
            earliest.OrderBy(x => x.End);
            return earliest[0];
        }

        private async Task<List<TimeInterval>> GetAllEarliestForDate(DateTime date)
        {
            List<TimeInterval> latest = new List<TimeInterval>();
            var latestReallocation = await _unitOfWork.EquipmentReallocationRepository.GetLastPendingForDay(date);
            var latestRenovation = await _unitOfWork.RenovationRepository.GetLastPendingForDay(date);
            var latestAppointment = await _unitOfWork.AppointmentRepository.GetLastForDate(DateTime.Now);
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
