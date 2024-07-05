using Biblioteca.Application.Interfaces.Presenters;
using Biblioteca.Application.Mapper;
using Biblioteca.Application.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.Application
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapperProfile));
            service.AddScoped<IBooksPresenter, BooksPresenter>();
            service.AddScoped<IUsersPresenter, UsersPresenter>();
            return service;
        }
    }
}
