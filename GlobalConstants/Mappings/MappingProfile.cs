using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ITCareerProject.Data;
using ITCareerProject.Services.DTOs.Events;
using ITCareerProject.Services.DTOs.Roles;
using ITCareerProject.Services.DTOs.Users;

namespace GlobalConstants.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, BaseUserDto>();
            CreateMap<BaseUserDto, ApplicationUser>();
            CreateMap<ApplicationUser, CreateUserDto>();
            CreateMap<CreateUserDto, ApplicationUser>();

            CreateMap<IdentityRole, BaseRoleDto>();
            CreateMap<IdentityRole, RoleDto>();
            CreateMap<RoleDto, BaseRoleDto>();
            CreateMap<BaseRoleDto, RoleDto>();

            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
            CreateMap<Event, CreateEventDto>();
            CreateMap<CreateEventDto, Event>();
        }
    }
}
