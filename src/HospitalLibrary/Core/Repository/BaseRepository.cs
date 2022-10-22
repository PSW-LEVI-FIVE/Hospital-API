using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Core.Repository.Interfaces;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class
    {
        protected readonly HospitalDbContext _dataContext;

        protected BaseRepository(HospitalDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Update(T entity)
        {
            _dataContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
        }

        public void Add(T entity)
        {
            _dataContext.Set<T>().Add(entity);
        }

        public int Save()
        {
            return _dataContext.SaveChanges();
        }

        public  T GetOne(T entity)
        {
            return  _dataContext.Set<T>().Find(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }
    }
}