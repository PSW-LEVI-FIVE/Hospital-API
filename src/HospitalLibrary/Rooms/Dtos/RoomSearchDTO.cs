using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Dtos
{
    public  class RoomSearchDTO
    {
        
        [Required] public RoomType RoomType { get; set; }
        [Required] public string RoomName { get; set; }

        public RoomSearchDTO()
        {
        }

        public RoomSearchDTO(RoomType roomType, string roomName)
        {
            RoomType = roomType;
            RoomName = roomName;
        }
    }
}
