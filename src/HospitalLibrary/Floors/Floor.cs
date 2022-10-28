using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Buildings;
using HospitalLibrary.Rooms;

namespace HospitalLibrary.Floors
{
    public class Floor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        public int Number { get; set; }
        
        public virtual ICollection<Room> Rooms { get; set; }

        public int BuildingId { get; set; }
        [ForeignKey("BuildingId")]
        public Building Building { get; set; }
    }
}