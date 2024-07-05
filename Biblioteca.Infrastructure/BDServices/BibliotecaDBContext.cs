using Biblioteca.Domain.POCOs.Context;
using Biblioteca.Infrastructure.BDServices.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.BDServices
{
    public class BibliotecaDBContext : DbContext
    {
        public BibliotecaDBContext(DbContextOptions options) : base(options) { }

        public DbSet<BooksModel> Books { get; set; }
        public DbSet<UsersModel> Users { get; set; }

        public DbSet<RolsModel> Rols { get; set; }
        public DbSet<RolsByUserModel> RolsByUser { get; set; }
        public DbSet<BooksByUserModel> BooksByUser { get; set; }
    }
}
