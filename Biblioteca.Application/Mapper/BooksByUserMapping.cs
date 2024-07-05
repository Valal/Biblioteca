using AutoMapper;
using Biblioteca.Domain.DTOs.Response.Data.Attributes;
using Biblioteca.Domain.POCOs.Context;

namespace Biblioteca.Application.Mapper
{
    public static class BooksByUserMapping
    {
        public static Profile AddBooksByUserMapping(this Profile profile)
        {
            profile.CreateMap<EntityBooksByUser, BooksByUserAttributes>()
                .ForMember(lpu => lpu.title, opt => opt.MapFrom(db => db.title))
                .ForMember(lpu => lpu.apa, opt => opt.MapFrom(db => db.apa));
            return profile;
        }
    }
}
