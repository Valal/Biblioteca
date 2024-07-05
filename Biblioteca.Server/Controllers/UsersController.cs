using Biblioteca.Application.Interfaces.Presenters;
using Biblioteca.Domain.DTOs.Request.Data;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Server.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersPresenter _usuarios;
        private readonly IBooksPresenter _libros;
        public UsersController(IUsersPresenter usuariosPresenter, IBooksPresenter libros)
        {
            _usuarios = usuariosPresenter;
            _libros = libros;
        }

        [HttpPost]
        [Route("[controller]/login")]
        public async Task<IActionResult> Login(UsersLoginData request)
        {
            var result = await _usuarios.Login(request);
            return _usuarios.errorResponse.errors.Any() ? BadRequest(_usuarios.errorResponse) : Ok(result);
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _usuarios.Get());
        }

        [HttpGet]
        [Route("[controller]/{user}")]
        public async Task<IActionResult> Get(string user)
        {
            return Ok(await _usuarios.GetById(user));
        }

        [HttpGet]
        [Route("[controller]/{user}/books")]
        public async Task<IActionResult> GetBooks(string user)
        {
            var result = await _usuarios.GetBooks(user);
            return _usuarios.errorResponse.errors.Count > 0 ? BadRequest(_usuarios.errorResponse) : Ok(result);
        }

        [HttpGet]
        [Route("[controller]/{user}/permissions")]
        public async Task<IActionResult> GetRole(string user)
        {
            return Ok(await _usuarios.IsAdmin(user));
        }

        [HttpGet]
        [Route("[controller]/{user}/books/{apa}/request")]
        public async Task<IActionResult> RequestBook(string user, string apa)
        {
            var result = await _usuarios.RequestBook(user, apa);
            return _usuarios.errorResponse.errors.Any() ? BadRequest(_usuarios.errorResponse) : Ok(result);
        }

        [HttpGet]
        [Route("[controller]/{user}/books/{apa}/deliver")]
        public async Task<IActionResult> DeliverBook(string user, string apa)
        {
            return Ok(await _libros.DeliverBook(user, apa));
        }
    }
}
