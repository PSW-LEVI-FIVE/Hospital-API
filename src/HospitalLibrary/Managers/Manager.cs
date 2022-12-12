using HospitalLibrary.BloodStorages;
using HospitalLibrary.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Managers
{
    public class Manager : Person
    {
        public Manager() { }

        public Manager(string name, string surname, string email, string uid, string phoneNumber, DateTime birthDate, Address address) : base(name, surname, email, uid, phoneNumber, birthDate, address)
        {
        }
    }
}
