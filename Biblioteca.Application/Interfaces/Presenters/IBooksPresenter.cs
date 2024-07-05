using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.DTOs.Response;

namespace Biblioteca.Application.Interfaces.Presenters
{
    public interface IBooksPresenter
    {
        public ErrorResponse errorResponse { get; set; }
        public ValueTask<BooksResponse> GetByParams(string apa, int year);
        public ValueTask<BooksResponse> Get();
        public ValueTask<BooksYearsResponse> GetYears();
        public ValueTask<ResultResponse> Post(BooksPostData resquest);
        public ValueTask<ResultResponse> Delete(string apa);
        public ValueTask<ResultResponse> DeliverBook(string user, string apa);
    }
}
