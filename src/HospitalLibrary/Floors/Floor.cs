using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Buildings;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Floors
{
    public class Floor : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get;private set; }
        public int Number { get;private set; }
        public float Area { get;private set; }
        
        public virtual ICollection<Room> Rooms { get;private set; }
        
        [ForeignKey("Building")]
        public int BuildingId { get;private set; }
        public Building Building { get;private set; }

        public Floor(int number, float area)
        {
            Number = number;
            Area = area;
        }

        public Floor(int id, float area, int buildingId)
        {
            Id = id;
            Area = area;
            BuildingId = buildingId;
        }
        public Floor(int buildingId)
        {
            BuildingId = buildingId;
        }
        
        
    }
}