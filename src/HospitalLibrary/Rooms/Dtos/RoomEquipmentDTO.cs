using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Dtos
{
    public class RoomEquipmentDTO
    {
       [Required] public string Name { get; set; }
       [Required] public int Quantity { get; set; }
       [Required] public int RoomId { get; set; }

        public RoomEquipmentDTO(string name, int quantity, int roomId)
        {
            Name = name;
            Quantity = quantity;
            RoomId = roomId;
        }
    }
}
