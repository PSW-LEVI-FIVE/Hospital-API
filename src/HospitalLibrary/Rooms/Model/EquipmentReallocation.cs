using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Model
{
    public class EquipmentReallocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public EquipmentReallocation() 
        {
        }
    }
}
