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
        
         public string RoomType { get; set; }
         public string RoomName { get; set; }

        public RoomSearchDTO()
        {
        }

        public RoomSearchDTO(string roomType, string roomName)
        {
            RoomType = roomType;
            RoomName = roomName;
        }
    }
}
