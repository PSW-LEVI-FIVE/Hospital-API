using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Rooms.Model
{
    public class RoomEquipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        
        public string Name { get; set; }
        
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

       
        public RoomEquipment() {}
        public RoomEquipment(int id, int quantity, string name, int roomId)
        {
            Id = id;
            Quantity = quantity;
            Name = name;
            RoomId = roomId;
        }

        public RoomEquipment(int quantity, string name, int roomId)
        {
            Quantity = quantity;
            Name = name;
            RoomId = roomId;
        }

        
    }
}