
using AutoMapper;
using Biblioteca.Domain.DTOs.Response.Data.Attributes;
using Biblioteca.Infrastructure.BDServices.Models;

namespace Biblioteca.Application.Mapper
{
    public static class UsersMapping
    {
        public static Profile AddUsersMapping(this Profile profile)
        {
            profile.CreateMap<UsersModel, UsersAttributes>()
                .ForMember(u => u.fullName, opt => opt.MapFrom(bd => bd.name+" "+bd.lastName))
                .ForMember(u => u.user, opt => opt.MapFrom(bd => bd.user));

            return profile;
        }
    }
}
