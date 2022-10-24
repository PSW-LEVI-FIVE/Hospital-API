using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Core.Model
{
    public class Room
    {
        
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Floor { get; set; }


        public Room(int id, string roomNumber, int floor)
        {
            Id = id;
            RoomNumber = roomNumber;
            Floor = floor;
        }
    }
}