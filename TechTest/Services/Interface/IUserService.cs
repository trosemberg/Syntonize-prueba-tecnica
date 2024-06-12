using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.DTO;
using TechTest.Models;

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
