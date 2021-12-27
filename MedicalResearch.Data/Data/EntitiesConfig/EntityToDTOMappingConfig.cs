using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManaging.Domain.Entities.Users;

namespace UserManaging.Infrastructure.Data.EntitiesConfig
{
    public class EntityToDTOMappingConfig : Profile
    {
        public EntityToDTOMappingConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
