using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Floors;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Buildings
{
    public class Building : BaseEntity
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        public virtual ICollection<Floor> Floors { get; private set; }

        public Building() { }

        public Building(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
        public Building(string name, string address)
        {
            Name = name;
            Address = address;
        }
        public Building(string name, string address, ICollection<Floor> floors)
        {
            Name = name;
            Address = address;
            Floors = floors;
        }
        public void UpdateName(string name)
        {
            if (name == null || name.Trim().Equals(""))
                throw new BadRequestException("You must enter new name!");
            Name = name;
        }
    }
}