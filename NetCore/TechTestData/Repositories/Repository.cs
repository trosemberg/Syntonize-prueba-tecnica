using Microsoft.EntityFrameworkCore;
using TechTestData.Repositories.Interface;
using TechTestData.Models;
using System.Xml.Linq;

namespace TechTestData.Repositories
{
    public class Repository<T> : IRepository<T> where T : TEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext dbcontext) 
        {
            _dbContext = dbcontext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync() 
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id) 
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetByNameAsync(string name)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Name.Equals(name));
        }

        public async Task InsertEntityAsync(T entity) 
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateEntityAsync(int id, T entity) 
        {
            var data = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteEntityAsync(int id) 
        {
            var data = await _dbSet.FindAsync(id);
            if (data == null)
            {
                return null;
            }

            _dbSet.Remove(data);
            await _dbContext.SaveChangesAsync();
            return data;
        }
    }
}