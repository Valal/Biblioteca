using Biblioteca.Application.Interfaces.Infrastructure.Context;
using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.POCOs.Context;
using Biblioteca.Infrastructure.BDServices;
using Biblioteca.Infrastructure.BDServices.Models;
using System.Linq;

namespace Biblioteca.Infrastructure.Context
{
    public class BooksContext : IBooksContext
    {
        private readonly BibliotecaDBContext _dbContext;
        public BooksContext(BibliotecaDBContext context) {
            _dbContext = context;
        }
        public List<BooksModel> Get(string apa, int years)
        {
            if (!string.IsNullOrEmpty(apa) && years == 0) return GetByApa(apa);
            else if (string.IsNullOrEmpty(apa) && years > 0) return GetByYears(years);
            else
                return _dbContext.Books.ToList();
        }

        private List<BooksModel> GetByYears(int years) => _dbContext.Books.Where(w => w.year == years).ToList();
        
        private List<BooksModel> GetByApa(string apa) => _dbContext.Books.Where(w => w.apa == apa).ToList(); 
        public async Task<List<int>> GetYears()
        {
            return _dbContext.Books.Select(s => s.year).Distinct().OrderBy(o => o).ToList();
        }
        public async Task<int> BorrowedBooks(string apa)
        {
            return _dbContext.BooksByUser.Where(w => w.apa == apa).Count();
        }
        public async Task<List<BooksModel>> GetRealAvailables(List<BooksModel> books)
        {
            books.ForEach(async s =>
            {
                s.availables = s.availables - await BorrowedBooks(s.apa);
            });

            return books;
        }
        public async Task<EntityResult> Post(BooksPostData request)
        {
            var libros = _dbContext.Set<BooksModel>();
            libros.Add(new BooksModel()
                {
                    name = request.data.name,
                    lastNames = request.data.lastNames,
                    apa = request.data.lastNames + ", (" + request.data.year.ToString() + ")",
                    editorial = request.data.editorial,
                    title = request.data.title,
                    year = request.data.year,
                    place = request.data.place,
                    availables = request.data.availables
                }
            );
            await _dbContext.SaveChangesAsync();
            return new EntityResult() { message = "Libro guardado de forma exitosa." };
        }
        public async Task<EntityResult> Delete(string apa)
        {
            var libro = _dbContext.Books.SingleOrDefault(w => w.apa == apa);
            if(libro == null)
                return new EntityResult() { message = "El libro no existe." };
            
            _dbContext.Books.Remove(libro);
            await _dbContext.SaveChangesAsync();
            return new EntityResult() { message = "Libro eliminado de forma exitosa." };
        }
        public async Task<bool> IsAvailable(string apa)
        {
            int occupied = await BorrowedBooks(apa);
            var libro = _dbContext.Books.Where(w => w.apa == apa).FirstOrDefault();
            return (libro.availables - occupied) > 0;
        }
        public async Task<EntityResult> DeliverBook(string user, string apa)
        {
            var idUsuario = _dbContext.Users.Where(w => w.user == user).Select(s => s.idUser).FirstOrDefault();
            var libros = _dbContext.BooksByUser.SingleOrDefault(w => w.apa == apa && w.idUser == idUsuario);
            if (libros == null)
                return new EntityResult() { message = "El libro ya ha sido entregado." };

            _dbContext.BooksByUser.Remove(libros);

            await _dbContext.SaveChangesAsync();
            return new EntityResult() { message = "Libro entregado a biblioteca." };
        }
    }
}
