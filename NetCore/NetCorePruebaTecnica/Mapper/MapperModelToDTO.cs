using AutoMapper;
using TechTest.DTO;
using TechTest.Models;

namespace TechTest.Mapper
{
    public class MapperModelToDTO : Profile
    {
        public MapperModelToDTO()
        {
            // Domain to DTO
            CreateMap<Users, UsersDTO>().ForMember(d => d.UserName, opt => opt.MapFrom(src => src.Name));
            CreateMap<Roles, RolesDTO>();

            // Dto to Domain 
            CreateMap<UsersDTO, Users>().ForMember(d => d.Name, opt => opt.MapFrom(src => src.UserName)); ;
            CreateMap<RolesDTO, Roles>();
        }
    }
}