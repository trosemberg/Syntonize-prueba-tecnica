using AutoMapper;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TechTest.DTO;
using TechTest.Models;
using TechTest.Repositories.Interface;
using TechTest.Services.Interface;

namespace TechTest.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<Roles> _rolesRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<Users> usersRepository, IRepository<Roles> rolesRepository, IMapper mapper) 
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsersDTO>> GetAllAsync()
        {
            var users = await _usersRepository.GetAllEntitiesAsync();
            return _mapper.Map<IEnumerable<UsersDTO>>(users);
        }

        public async Task<UsersDTO> GetByIdAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            return _mapper.Map<UsersDTO>(user); ;
        }

        public async Task<UsersDTO> UpdateAsync(UsersDTO usersDTO)
        {
            var user = _mapper.Map<Users>(usersDTO);
            var role = await _rolesRepository.GetByNameAsync(usersDTO.Role);
            user.RolesId = role?.Id;
            var dbUser = await _usersRepository.UpdateEntityAsync(user.Id, user);
            return _mapper.Map<UsersDTO>(dbUser);
        }

        public async Task InsertAsync(UsersDTO usersDTO)
        {
            var user = _mapper.Map<Users>(usersDTO);

            Random rand = new Random();
            var salt = rand.Next(50000).ToString();

            user.Password = EncryptPassword(user.Password, salt);

            user.Salt = salt;

            var role = await _rolesRepository.GetByNameAsync(usersDTO.Role);
            user.RolesId = role?.Id;
            await _usersRepository.InsertEntityAsync(user);
        }

        public async Task<UsersDTO> DeleteAsync(int id)
        {
            var dbUser = await _usersRepository.DeleteEntityAsync(id);
            return _mapper.Map<UsersDTO>(dbUser);
        }

        public async Task<string> LoginUserAsync(LoginDTO login)
        {
            var dbUser = await _usersRepository.GetByNameAsync(login.UserName);
            var encryptRecievedPassword = EncryptPassword(login.Password,dbUser.Salt);

            var loginSuccess = encryptRecievedPassword.Equals(dbUser.Password);
            if (!loginSuccess)
                return null;
            var roleName = await _rolesRepository.GetByIdAsync(dbUser.RolesId.Value);

            var jwtToken = Authentication.Authentication.GenerateJWTAuthetication(login.UserName, roleName.Name);
            var validUserName = Authentication.Authentication.ValidateToken(jwtToken);
            return jwtToken;
        }

        private string EncryptPassword(string password, string salt) 
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                return String.Concat(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password + salt))
                    .Select(item => item.ToString("x2")));
            }
        }
    }
}