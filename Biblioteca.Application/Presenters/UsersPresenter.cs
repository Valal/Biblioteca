using AutoMapper;
using Biblioteca.Application.Interfaces.Infrastructure;
using Biblioteca.Application.Interfaces.Presenters;
using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.DTOs.Response;
using Biblioteca.Domain.DTOs.Response.Data;
using Biblioteca.Domain.DTOs.Response.Data.Attributes;
using System.Net;

namespace Biblioteca.Application.Presenters
{
    public class UsersPresenter : IUsersPresenter
    {
        private readonly IMapper _mapper;
        private IBibliotecaContextRepository _repository { get; set; }
        public ErrorResponse errorResponse { get; set; }

        public UsersPresenter(IBibliotecaContextRepository repository, IMapper mapper) {
            _mapper = mapper;
            _repository = repository;
            errorResponse = new ErrorResponse();
            errorResponse.errors = new List<Errors>();
        }

        public async ValueTask<UsersResponseSingle> Login(UsersLoginData data)
        {
            if (await IsLoginValid(data) || await Exists(data))
            {
                var result = await _repository.UsersContext.Login(data);
                if (result != null)
                {
                    var dto = _mapper.Map<UsersAttributes>(result);
                    return new UsersResponseSingle() { data = new UsersDataSingle() { attributes = dto, type = "usuarios" } };
                }
            }
            errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.Unauthorized, detail = "Credenciales incorrectas.", title = "Usuario no autorizado" });
            return null;
        }

        public async ValueTask<UsersResponse> Get()
        {
            var result = await _repository.UsersContext.Get(string.Empty);
            var dto = _mapper.Map<List<UsersAttributes>>(result);
            return new UsersResponse() { data = new UsersData() { type = "usuarios", attributes = dto } };
        }

        public async ValueTask<UsersResponseSingle> GetById(string user)
        {
            var result = await _repository.UsersContext.Get(user);
            var dto = _mapper.Map<UsersAttributes>(result.FirstOrDefault());
            return new UsersResponseSingle() { data = new UsersDataSingle() { type = "usuarios", attributes = dto } };
        }

        public async ValueTask<UsersBooksResponse> GetBooks(string user)
        {
            var result = await _repository.UsersContext.GetBooks(user);
            if(result.Count > 0)
            {
                var dto = _mapper.Map<List<BooksByUserAttributes>>(result);
                return new UsersBooksResponse() { data = new UsersBooksData() { type = "libros-usuarios", attributes = dto } };
            }
            errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.NotFound, detail = "No cuenta con libros préstados", title = "No se encontro información" });
            return new UsersBooksResponse();
        }

        public async ValueTask<UsersAdminResponse> IsAdmin(string user)
        {
            var result = await _repository.UsersContext.IsAdmin(user);
            return new UsersAdminResponse()
            {
                data = new UsersAdminData()
                { type = "usuario", attributes = new UsersAdminAttributes() { isAdmin = result } }
            };
        }
        public async ValueTask<ResultResponse> RequestBook(string user, string apa)
        {
            if(!await _repository.UsersContext.PriorRequest(user, apa))
            {
                if (await _repository.BooksContext.IsAvailable(apa))
                {
                    var result = await _repository.UsersContext.RequestBook(user, apa);
                    return new ResultResponse() { data = new ResultData() { attributes = new ResultAttributes { message = result.message } } };
                }
                else                
                    errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "El libro seleccionado, no se encuentra disponible", title = "Selección no disponible" });
            }
            else
                errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.BadRequest, detail = "Ya ha solicitado anteriormente este libro.", title = "Selección no disponible" });

            return null;
        }

        private async Task<bool> IsLoginValid(UsersLoginData data)
        {
            if(!string.IsNullOrEmpty(data.attributes.usuario) && !string.IsNullOrEmpty(data.attributes.password))
            {
                return true;
            }
            errorResponse.errors.Add(new Errors() { code = (int)HttpStatusCode.Unauthorized, detail = "Usuario o contraseña vacíos", title = "Usuario no autorizado" });
            return false;
        }
        private async Task<bool> Exists(UsersLoginData data)
        {
            var result = await _repository.UsersContext.Get(data.attributes.usuario);
            return result.Count > 0;
        }
    }
}
