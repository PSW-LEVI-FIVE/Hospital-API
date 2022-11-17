using HospitalLibrary.Rooms.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.DTOs
{
    public class CreateIntervalsEquipmentReallocationDTO
    {
        [Required]
        public int StartingRoomId { get; set; }

        [Required]
        public int DestinationRoomId { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        public int duration { get; set; }
        
    }
}
