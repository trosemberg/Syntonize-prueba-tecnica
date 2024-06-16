using TechTest.DTO;

namespace TechTest.Services.Interface
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductsDTO>> GetAllAsync();

        Task<ProductsDTO> GetByIdAsync(int id);

        Task<ProductsDTO> UpdateAsync(ProductsDTO product);

        Task InsertAsync(ProductsDTO product);

        Task<ProductsDTO> DeleteAsync(int id);
    }
}
