
using Biblioteca.Application.Interfaces.Infrastructure.Context;

namespace Biblioteca.Application.Interfaces.Infrastructure
{
    public interface IBibliotecaContextRepository
    {
        public IUsersContext UsersContext { get; }
        public IBooksContext BooksContext { get; }
    }
}
