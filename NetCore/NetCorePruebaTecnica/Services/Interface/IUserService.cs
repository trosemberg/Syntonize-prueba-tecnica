using TechTest.DTO;

namespace TechTest.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UsersDTO>> GetAllAsync();

        Task<UsersDTO> GetByIdAsync(int id);

        Task<UsersDTO> UpdateAsync(UsersDTO users);

        Task InsertAsync(UsersDTO users);

        Task<UsersDTO> DeleteAsync(int id);

        Task<string> LoginUserAsync(LoginDTO login);
    }
}
