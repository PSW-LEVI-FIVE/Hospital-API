using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Persons.Interfaces
{
    public interface IPersonRepository
    {
        public Person GetOneByUid(string uid);
        public Person GetOneByEmail(string email);
    }
}