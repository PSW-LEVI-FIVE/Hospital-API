using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Floors;

namespace HospitalLibrary.Rooms
{
    public class Room
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        public string RoomNumber { get; set; }
        
        public int FloorId { get; set; }
        [ForeignKey("FloorId")]
        public Floor Floor { get; set; }
        
    }
}