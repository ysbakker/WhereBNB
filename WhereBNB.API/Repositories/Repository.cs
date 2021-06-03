using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhereBNB.API.Model;

namespace WhereBNB.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private WhereBNBContext Context { get; set; }
        protected DbSet<T> Table { get; set; }

        public Repository(WhereBNBContext context)
        {
            Context = context;
            Table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Table.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<IEnumerable<T>> Get(int page, int pageSize)
        {
            int skip = page * pageSize - pageSize;
            return await Table.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<int> Count()
        {
            return await Table.CountAsync();
        }

        public async Task Insert(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            Table.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(object id)
        {
            var existing = await Table.FindAsync(id);
            Table.Remove(existing);
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }
    }
}