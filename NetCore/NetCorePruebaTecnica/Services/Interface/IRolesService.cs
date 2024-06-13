using TechTest.DTO;

namespace TechTest.Services.Interface
{
    public interface IRolesService
    {
        Task<IEnumerable<RolesDTO>> GetAllAsync();

        Task<RolesDTO> GetByIdAsync(int id);

        Task<RolesDTO> UpdateAsync(RolesDTO roles);

        Task InsertAsync(RolesDTO roles);

        Task<RolesDTO> DeleteAsync(int id);
    }
}
