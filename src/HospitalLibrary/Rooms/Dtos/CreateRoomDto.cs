using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Rooms.Dtos
{
    public class CreateRoomDto
    {
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        public float Area { get; set; }
        
    }
}