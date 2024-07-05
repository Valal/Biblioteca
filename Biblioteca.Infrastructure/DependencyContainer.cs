using Biblioteca.Application.Interfaces.Infrastructure;
using Biblioteca.Infrastructure.BDServices;
using Biblioteca.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Biblioteca.Infrastructure
{
    public static class DependencyContianer
    { 
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BibliotecaDBContext>(options => 
                options.UseInMemoryDatabase("Biblioteca")
            );

            services.AddScoped<IBibliotecaContextRepository, BibliotecaContextRepository>();
            return services;
        }
    }
}
