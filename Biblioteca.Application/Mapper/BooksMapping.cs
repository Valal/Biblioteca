
using AutoMapper;
using Biblioteca.Domain.DTOs.Response.Data.Attributes;
using Biblioteca.Infrastructure.BDServices.Models;

namespace Biblioteca.Application.Mapper
{
    public static class BooksMapping
    {
        public static Profile AddBooksMapping(this Profile profile)
        {
            profile.CreateMap<BooksModel, BooksAttributes>()
                .ForMember(l => l.title, opt => opt.MapFrom(bd => bd.title))
                .ForMember(l => l.editorial, opt => opt.MapFrom(bd => bd.editorial))
                .ForMember(l => l.lastNames, opt => opt.MapFrom(bd => bd.lastNames))
                .ForMember(l => l.name, opt => opt.MapFrom(bd => bd.name))
                .ForMember(l => l.place, opt => opt.MapFrom(bd => bd.place))
                .ForMember(l => l.apa, opt => opt.MapFrom(bd => bd.apa))
                .ForMember(l => l.year, opt => opt.MapFrom(bd => bd.year))
                .ForMember(l => l.availables, opt => opt.MapFrom(bd => bd.availables));
            
            return profile;
        }
    }
}
