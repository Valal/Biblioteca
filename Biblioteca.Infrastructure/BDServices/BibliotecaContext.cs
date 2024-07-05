
using Biblioteca.Domain.POCOs.Context;
using Biblioteca.Infrastructure.BDServices.Models;
using Biblioteca.Utilities.Helpers;

namespace Biblioteca.Infrastructure.BDServices
{
    public class BibliotecaContext
    {
        public BibliotecaContext(BibliotecaDBContext context)
        {
            if (!context.Books.Any())
            {
                context.Books.AddRange(Seed.SeedData<BooksModel>("Books.json", "Seeds", true));
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(Seed.SeedData<UsersModel>("Users.json", "Seeds", true));
            }
            if (!context.Rols.Any())
            {
                context.Rols.AddRange(Seed.SeedData<RolsModel>("Rols.json", "Seeds", true));
            }
            if (!context.RolsByUser.Any())
            {
                context.RolsByUser.AddRange(Seed.SeedData<RolsByUserModel>("RolsByUser.json", "Seeds", true));
            }
            if (!context.BooksByUser.Any())
            {
                context.BooksByUser.AddRange(Seed.SeedData<BooksByUserModel>("BooksByUser.json", "Seeds", true));
            }

            context.SaveChanges();
        }
    }
}
