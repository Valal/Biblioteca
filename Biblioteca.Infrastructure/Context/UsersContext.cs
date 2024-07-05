using Biblioteca.Application.Interfaces.Infrastructure.Context;
using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.DTOs.Response;
using Biblioteca.Domain.POCOs.Context;
using Biblioteca.Infrastructure.BDServices;
using Biblioteca.Infrastructure.BDServices.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Infrastructure.Context
{
    public class UsersContext : IUsersContext
    {
        private readonly BibliotecaDBContext _db;
        public UsersContext(BibliotecaDBContext context) {
            _db = context;
        }

        public async Task<List<UsersModel>> Get(string user)
        {
            if(!string.IsNullOrEmpty(user))
            {
                return _db.Users.Where(w => w.user == user).ToList();
            }
            else
            {
                return _db.Users.ToList();
            }
        }
        public async Task<UsersModel> Login(UsersLoginData data)
        {
            if (_db.Users.Where(w => w.password == data.attributes.password && w.user == data.attributes.usuario).Any())
                return _db.Users.Where(w => w.password == data.attributes.password && w.user == data.attributes.usuario).FirstOrDefault();

            return null;
        }
        public async Task<List<EntityBooksByUser>> GetBooks(string user)
        {
            if (await ExistUser(user))
            {
                return (from lxu in _db.BooksByUser.ToList()
                             join l in _db.Books.ToList() on lxu.apa equals l.apa
                             join u in _db.Users.ToList() on lxu.idUser equals u.idUser
                             where u.user == user
                             select new EntityBooksByUser
                             {
                                 apa = l.apa,
                                 title = l.title
                             }).ToList();
            }
            return new List<EntityBooksByUser>();
        }
        public async Task<bool> IsAdmin(string user)
        {
            var usr = _db.Users.Where(w => w.user == user).Select(s => s.idUser).FirstOrDefault();
            return _db.RolsByUser.Where(w => w.idUser == usr).Any();
        }
        public async Task<EntityResult> RequestBook(string user, string apa)
        {
            var booksByUser = _db.Set<BooksByUserModel>();
            booksByUser.Add(new BooksByUserModel()
            {
                apa = apa,
                idUser = _db.Users.Where(w => w.user == user).Select(s => s.idUser).FirstOrDefault(),
                idBookByUser = _db.BooksByUser.Max(i => i.idBookByUser)+1,
            });
            await _db.SaveChangesAsync();
            return new EntityResult() { message = "Libro adquirido de forma exitosa." };
        }

        public async Task<bool> PriorRequest(string user, string apa)
        {
            var idUsuario = _db.Users.Where(w => w.user == user).Select(s => s.idUser).FirstOrDefault();
            return _db.BooksByUser.Where(w => w.apa == apa && w.idUser == idUsuario).Any();
        }
        
        private async Task<bool> ExistUser(string user)
        {
            return _db.Users.Where(w => w.user == user).Any();
        }
    }
}
