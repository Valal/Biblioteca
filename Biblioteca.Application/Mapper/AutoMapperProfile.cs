
using AutoMapper;

namespace Biblioteca.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            BooksByUserMapping.AddBooksByUserMapping(this);
            UsersMapping.AddUsersMapping(this);
            BooksMapping.AddBooksMapping(this);
        }
    }
}
