using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Buildings;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Floors
{
    public class Floor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public int Number { get; set; }
        public float Area { get; set; }
        
        public virtual ICollection<Room> Rooms { get; set; }
        
        [ForeignKey("Building")]
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        
        public Floor() {}
    }
}