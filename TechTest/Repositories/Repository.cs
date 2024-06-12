using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using TechTest.Data;
using TechTest.Repositories.Interface;
using TechTest.Models;

namespace TechTest.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: TechTest.Models.TEntity
    {
        private readonly DBContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DBContext dbcontext) 
        {
            _dbContext = dbcontext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync() 
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id) 
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetByNameAsync(string name)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Name.Equals(name));
        }

        public async Task InsertEntityAsync(TEntity entity) 
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateEntityAsync(int id, TEntity entity) 
        {
            var data = await _dbSet.FindAsync(id);
            if (data == null)
            {
                return null;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteEntityAsync(int id) 
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