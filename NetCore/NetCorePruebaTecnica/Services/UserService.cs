using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TechTest.Configuration;
using TechTest.DTO;
using TechTestData.Models;
using TechTestData.Repositories.Interface;
using TechTest.Services.Interface;

namespace TechTest.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<Roles> _rolesRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IRepository<Users> usersRepository, IRepository<Roles> rolesRepository, IMapper mapper, IOptions<AppSettings> appSettings) 
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
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
            var role = await _rolesRepository.GetByIdAsync(usersDTO.RolesId!.Value);
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

            var role = await _rolesRepository.GetByIdAsync(usersDTO.RolesId!.Value);
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

            var jwtToken = GenerateJWTAuthetication(login.UserName, roleName.Name);
            return jwtToken;
        }

        private string GenerateJWTAuthetication(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtHeaderParameterNames.Jku, userName),
                new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, userName)
            };

            claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Convert.ToString(_appSettings.JwtKey)));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(1);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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