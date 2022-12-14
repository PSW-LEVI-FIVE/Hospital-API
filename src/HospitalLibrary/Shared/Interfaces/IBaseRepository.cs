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
        int Create(T entity);
        
        int UpdateAndSave(T entity);
        T GetOne(int key);
        Task<IEnumerable<T>> GetAll();
    }
}