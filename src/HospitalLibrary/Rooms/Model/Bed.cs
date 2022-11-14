using System.Collections.Generic;
using HospitalLibrary.Hospitalizations;

namespace HospitalLibrary.Rooms.Model
{
    public class Bed: Rooms.Model.RoomEquipment
    {
        public int Number { get; set; }

        public List<Hospitalization> Hospitalizations { get; set; }
        
        public Bed(int id, int quantity, string name, int roomId, int number) : base(id, quantity, name, roomId)
        {
            Number = number;
        }
    }
}