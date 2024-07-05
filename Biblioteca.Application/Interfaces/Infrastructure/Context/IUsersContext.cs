using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.POCOs.Context;
using Biblioteca.Infrastructure.BDServices.Models;

namespace Biblioteca.Application.Interfaces.Infrastructure.Context
{
    public interface IUsersContext
    {
        public Task<List<UsersModel>> Get(string user);
        public Task<UsersModel> Login(UsersLoginData data);
        public Task<bool> IsAdmin(string user);
        public Task<List<EntityBooksByUser>> GetBooks(string user);
        public Task<EntityResult> RequestBook(string user, string apa);
        public Task<bool> PriorRequest(string user, string apa);
    }
}
