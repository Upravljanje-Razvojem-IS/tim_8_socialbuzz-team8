using AutoMapper;
using System;
using UserService.Dtos;
using UserService.Entities;
using UserService.Models.Dtos.Role;
using UserService.Models.Dtos.User;

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
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<UserCreateDto, ApplicationUser>();
            CreateMap<UserUpdateDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserCreateDto>();
            CreateMap<Object, ApplicationUser>();

        }
    }
}