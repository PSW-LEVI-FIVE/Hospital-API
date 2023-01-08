using System.Collections.Generic;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Rooms.Model
{
    public class Bed: RoomEquipment
    {
        public int Number { get; set; }

        public List<Hospitalization> AllHospitalizations { get; set; }

        
        
        public Bed(int id, int quantity, string name, int roomId, int number) : base(id, quantity, name, roomId)
        {
            Number = number;
        }
    }
}