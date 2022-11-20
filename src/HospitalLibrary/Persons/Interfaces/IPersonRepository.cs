using HospitalLibrary.Managers;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Persons.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        public Person GetOneByUid(string uid);
        public Person GetOneByEmail(string email);
    }
}