using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.POCOs.Context;
using Biblioteca.Infrastructure.BDServices.Models;

namespace Biblioteca.Application.Interfaces.Infrastructure.Context
{
    public interface IBooksContext
    {
        public List<BooksModel> Get(string apa, int year);
        public Task<List<int>> GetYears();
        public Task<int> BorrowedBooks(string apa);
        public Task<List<BooksModel>> GetRealAvailables(List<BooksModel> books);
        public Task<EntityResult> Post(BooksPostData request);
        public Task<EntityResult> Delete(string apa);
        public Task<bool> IsAvailable(string apa);
        public Task<EntityResult> DeliverBook(string user, string apa);
    }
}
