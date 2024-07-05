using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.DTOs.Response;

namespace Biblioteca.Application.Interfaces.Presenters
{
    public interface IUsersPresenter
    {
        public ErrorResponse errorResponse {  get; set; }
        public ValueTask<UsersResponseSingle> Login(UsersLoginData data);
        public ValueTask<UsersResponse> Get();
        public ValueTask<UsersResponseSingle> GetById(string user);
        public ValueTask<UsersBooksResponse> GetBooks(string user);
        public ValueTask<UsersAdminResponse> IsAdmin(string user);
        public ValueTask<ResultResponse> RequestBook(string user, string apa);
    }
}
