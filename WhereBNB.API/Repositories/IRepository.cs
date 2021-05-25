using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhereBNB.API.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
        Task Insert(T entity);
        void Update(T entity);
        Task Delete(object id);
        Task Save();
    }
}