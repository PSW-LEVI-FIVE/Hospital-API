using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Shared.Repository
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

        public int UpdateAndSave(T entity)
        {
            Update(entity);
            return Save();
        }

        public T GetOne(int key)
        {
            return  _dataContext.Set<T>().Find(key);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public int Create(T entity)
        {
            Add(entity);
            return Save();
        }
    }
}