using HospitalLibrary.Appointments;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients;
using HospitalLibrary.Rooms.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.DTOs
{
    public class CreateEquipmentReallocationDTO
    {
        [Required]
        public int StartingRoomId { get; set; }

        [Required]
        public int DestinationRoomId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int equipmentId { get; set; }
        
        [Required]
        public int amount { get; set; }

        public CreateEquipmentReallocationDTO(int startingRoomId, int destinationRoomId, DateTime startDate, DateTime endDate, int equipmentId, int amount)
        {
            StartingRoomId = startingRoomId;
            DestinationRoomId = destinationRoomId;
            StartDate = startDate;
            EndDate = endDate;
            this.equipmentId = equipmentId;
            this.amount = amount;
        }

        public EquipmentReallocation MapToModel()
        {
            return new EquipmentReallocation
            {
                StartingRoomId = StartingRoomId,
                DestinationRoomId = DestinationRoomId,
                StartAt = StartDate,
                EndAt = EndDate,
                EquipmentId = equipmentId,
                amount= amount,
                state = ReallocationState.PENDING
            };
        }
    }
}
