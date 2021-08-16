using AutoMapper;
using UserService.Entities;
using UserService.Models.Dtos.Role;

namespace UserService
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleDto, ApplicationRole>();
            CreateMap<RoleCreateDto, ApplicationRole>();
            CreateMap<RoleUpdateDto, ApplicationRole>();
            CreateMap<ApplicationRole, RoleDto>();
        }
    }
}