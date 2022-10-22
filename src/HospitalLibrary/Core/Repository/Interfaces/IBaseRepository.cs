using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);

        int Save();
        T GetOne(T entity);
        Task<IEnumerable<T>> GetAll();
    }
}