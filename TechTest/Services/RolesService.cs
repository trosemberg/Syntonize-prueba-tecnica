using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTest.DTO;
using TechTest.Models;
using TechTest.Repositories.Interface;
using TechTest.Services.Interface;

namespace TechTest.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRepository<Roles> _rolesRepository;
        private readonly IMapper _mapper;

        public RolesService(IRepository<Roles> rolesRepository, IMapper mapper) 
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RolesDTO>> GetAllAsync()
        {
            var roles = await _rolesRepository.GetAllEntitiesAsync();
            return _mapper.Map<IEnumerable<RolesDTO>>(roles);
        }

        public async Task<RolesDTO> GetByIdAsync(int id)
        {
            var role = await _rolesRepository.GetByIdAsync(id);
            return _mapper.Map<RolesDTO>(role);
        }

        public async Task<RolesDTO> UpdateAsync(RolesDTO rolesDTO)
        {
            var roles = _mapper.Map<Roles>(rolesDTO);
            var dbRole = await _rolesRepository.UpdateEntityAsync(roles.Id, roles);
            return _mapper.Map<RolesDTO>(dbRole);
        }

        public async Task InsertAsync(RolesDTO rolesDTO)
        {
            var role = _mapper.Map<Roles>(rolesDTO);
            await _rolesRepository.InsertEntityAsync(role);
        }

        public async Task<RolesDTO> DeleteAsync(int id)
        {
            var dbRole = await _rolesRepository.DeleteEntityAsync(id);
            return _mapper.Map<RolesDTO>(dbRole);
        }
    }
}