using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest.Repositories.Interface
{
    public interface IRepository<TEntity> where TEntity : TechTest.Models.TEntity
    {
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByNameAsync(string name);

        Task InsertEntityAsync(TEntity entity);

        Task<TEntity> UpdateEntityAsync(int id, TEntity entity);

        Task<TEntity> DeleteEntityAsync(int id);
    }
}
