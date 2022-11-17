using HospitalLibrary.Appointments;
using HospitalLibrary.Migrations;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Interfaces;
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

        public async Task<IEnumerable<TimeInterval>> GetPossibleInterval(int Starting_roomId, int Destination_roomId,DateTime date, TimeSpan duration)
        {
            Task<IEnumerable<TimeInterval>> intervalsA = getIntevals(Starting_roomId,date); 
            Task<IEnumerable<TimeInterval>> intervalsB = getIntevals(Destination_roomId,date);

            return null;      
        }

        public Task<IEnumerable<TimeInterval>> GetPossibleInterval(int Starting_roomId, int Destination_roomId, TimeSpan duration)
        {
            return null;
        }

        async Task<IEnumerable<TimeInterval>> getIntevals(int roomId, DateTime date) {

            IEnumerable<TimeInterval> roomTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(roomId,date) ;
            IEnumerable<TimeInterval> roomReallocationsIntervals =
                await _unitOfWork.EquipmentReallocationRepository.GetAllRoomTakenInrevalsForDate(roomId,date);


            IEnumerable<TimeInterval> mixedIntervals = roomReallocationsIntervals.Concat(roomTimeIntervals);

            return mixedIntervals;
            
        }

    }
}
