using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Buildings;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Floors
{
    public class Floor : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get;private set; }
        public int Number { get;private set; }
        public Area Area { get;private set; }
        
        public virtual ICollection<Room> Rooms { get;private set; }
        
        [ForeignKey("Building")]
        public int BuildingId { get;private set; }
        public Building Building { get;private set; }

        public Floor(int number, Area area)
        {
            Number = number;
            Area = area;
        }

        public Floor(int id, Area area, int buildingId)
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