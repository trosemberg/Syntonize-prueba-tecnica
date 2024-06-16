using TechTestData.Models;

namespace TechTestData.Repositories.Interface
{
    public interface IRepository<T> where T : TEntity
    {
        Task<IEnumerable<T>> GetAllEntitiesAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> GetByNameAsync(string name);

        Task InsertEntityAsync(T entity);

        Task<T> UpdateEntityAsync(int id, T entity);

        Task<T> DeleteEntityAsync(int id);
    }
}
