using Biblioteca.Application.Interfaces.Infrastructure;
using Biblioteca.Application.Interfaces.Infrastructure.Context;
using Biblioteca.Infrastructure.BDServices;
using Biblioteca.Infrastructure.Context;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.Infrastructure.Repository
{
    public class BibliotecaContextRepository : IBibliotecaContextRepository
    {
        private BibliotecaContext bibliotecaDB;
        private readonly BibliotecaDBContext bibliotecaDBContext;
        
        public BibliotecaContextRepository(BibliotecaDBContext context, IConfiguration configuration) {
            bibliotecaDBContext = context;
            bibliotecaDB = new BibliotecaContext(context);
        }

        public IBooksContext BooksContext => new BooksContext(bibliotecaDBContext);
        public IUsersContext UsersContext => new UsersContext(bibliotecaDBContext);
    }
}
