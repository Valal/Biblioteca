using AutoMapper;
using Biblioteca.Application.Interfaces.Infrastructure;
using Biblioteca.Application.Interfaces.Presenters;
using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.DTOs.Response;
using Biblioteca.Domain.DTOs.Response.Data;
using Biblioteca.Domain.DTOs.Response.Data.Attributes;
using Biblioteca.Infrastructure.BDServices.Models;
using System.Net;

namespace Biblioteca.Application.Presenters
{
    public class BooksPresenter : IBooksPresenter
    {
        public ErrorResponse errorResponse { get; set; }
        
        private readonly IMapper _mapper;
        private IBibliotecaContextRepository _repository { get; set; }
        public BooksPresenter(IBibliotecaContextRepository repository, IMapper mapper)
        {
            
            errorResponse = new ErrorResponse();
            errorResponse.errors = new List<Errors>();
            _repository = repository;
            _mapper = mapper;
        }

        public async ValueTask<BooksResponse> Get()
        {
            var result = _repository.BooksContext.Get(string.Empty, 0);
            if (result == null)
            {
                errorResponse.errors.Add(new Errors() { code = 404, detail = "No existen libros disponibles", title = "No existe información disponible" });
                return null;
            }
            result = await _repository.BooksContext.GetRealAvailables(result);

            var dto = _mapper.Map<List<BooksAttributes>>(result);
            return new BooksResponse() { data = new BooksData() { type = "Libros", attributes = dto } };

        }
        public async ValueTask<BooksResponse> GetByParams(string apa, int year)
        {
            var result = _repository.BooksContext.Get(apa, year);
            if (result.Count == 0)
            {
                errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.NotFound, title = "El libro no existe", detail = "No se encontro el libro solicitado." });
                return null;
            }

            result = await _repository.BooksContext.GetRealAvailables(result);
            var dto = _mapper.Map<List<BooksAttributes>>(result);
            return new BooksResponse() { data = new BooksData() { type = "Libros", attributes = dto } };
        }

        public async ValueTask<BooksYearsResponse> GetYears()
        {
            var result = await _repository.BooksContext.GetYears();
            var dto = (from y in result select new BookYearsAttributes() { year = y }).ToList();
            return new BooksYearsResponse() { data = new BookYearsData() { type = "libros", attributes = dto } };
        }
        public async ValueTask<ResultResponse> Post(BooksPostData request)
        {
            await IsValid(request);                

            if(await Exists(request.data.lastNames + ", (" + request.data.year.ToString() + ")"))
               errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "El libro ya existe", title = "Recurso disponible." });

            if (errorResponse.errors.Count > 0) return null;

            var result = await _repository.BooksContext.Post(request);
            return new ResultResponse() { data = new ResultData() { attributes = new ResultAttributes() { message = result.message } } }; 
        }
        public async ValueTask<ResultResponse> Delete(string apa)
        {
            if (await Exists(apa))
            {
                var result = await _repository.BooksContext.Delete(apa);
                return new ResultResponse() { data = new ResultData() { attributes = new ResultAttributes() { message = result.message } } };
            }
            errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "El libro no existe", title = "Recurso no disponible." });
            return null;
        }
        public async ValueTask<ResultResponse> DeliverBook(string user, string apa)
        {
            var result = await _repository.BooksContext.DeliverBook(user, apa);
            return new ResultResponse() { data = new ResultData() { attributes = new ResultAttributes() { message = result.message } } };
        }
        private async Task IsValid(BooksPostData request)
        {
            if (string.IsNullOrEmpty(request.data.editorial)) { errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "Se requiere editorial", title = "Valores vacíos no admitidos" }); }
            if (string.IsNullOrEmpty(request.data.name)) { errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "Se requiere nombre", title = "Valores vacíos no admitidos" }); }
            if (string.IsNullOrEmpty(request.data.lastNames)) { errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "Se requiere apellidos", title = "Valores vacíos no admitidos" }); }
            if (string.IsNullOrEmpty(request.data.place)) { errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "Se requiere procedencia", title = "Valores vacíos no admitidos" }); }
            if (string.IsNullOrEmpty(request.data.title)) { errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "Se requiere título", title = "Valores vacíos no admitidos" }); }
            if (!await IsValidYear(request.data.year)) { errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "Se requiere año", title = "Valores vacíos no admitidos" }); }
            if (!await IsValidAvailable(request.data.availables)) { request.data.availables = 1; }
        }
        private async Task<bool> Exists(string apa)
        {
            var result = _repository.BooksContext.Get(apa, 0);
            return result.Count > 0;
        }
        private async Task<bool> IsValidAvailable(int available)
        {
            return available > 0 ? true : false;
        }
        private async Task<bool> IsValidYear(int year)
        {
            return year > 0; 
        }
    } 
}
