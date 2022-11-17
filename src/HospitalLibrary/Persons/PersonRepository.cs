using System.Linq;
using HospitalLibrary.Persons.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Persons
{
    public class PersonRepository: BaseRepository<Person>,IPersonRepository
    {
        public PersonRepository(HospitalDbContext dataContext) : base(dataContext){}
        public Person GetOneByUid(string uid)
        {
            return _dataContext.Persons.Where(p => p.Uid.Equals(uid)).FirstOrDefault();
        }
        public Person GetOneByEmail(string email)
        {
            return _dataContext.Persons.Where(p => p.Email.Equals(email)).FirstOrDefault();
        }
        
    }
}