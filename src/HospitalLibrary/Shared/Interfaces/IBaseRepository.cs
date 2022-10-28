using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface IBaseRepository<T>
    {
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);

        int Save();
        T GetOne(int key);
        Task<IEnumerable<T>> GetAll();
    }
}