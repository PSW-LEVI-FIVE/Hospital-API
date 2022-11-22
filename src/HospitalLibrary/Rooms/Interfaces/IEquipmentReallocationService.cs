using HospitalLibrary.Appointments;
using HospitalLibrary.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IEquipmentReallocationService
    {
        Task<IEnumerable<EquipmentReallocation>> GetAll();

        Task<IEnumerable<EquipmentReallocation>> GetByRoom(int roomId);
        Task Delete(int id);
        
        Task<List<TimeInterval>> GetPossibleInterval(int Starting_roomId,int Destination_roomId,DateTime date, TimeSpan duration);
        
    }
}
