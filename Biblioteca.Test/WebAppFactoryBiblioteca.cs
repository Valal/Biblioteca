using Biblioteca.Application.Interfaces.Presenters;
using Biblioteca.Application.Presenters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.Test
{
    public class WebAppFactoryBiblioteca<TProgram> : WebApplicationFactory<Program> where TProgram : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IBooksPresenter, BooksPresenter>();
                services.AddScoped<IUsersPresenter, UsersPresenter>();
            });
        }
    }
}