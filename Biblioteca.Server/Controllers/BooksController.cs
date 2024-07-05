using Biblioteca.Application.Interfaces.Presenters;
using Biblioteca.Domain.DTOs.Request.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Biblioteca.Server.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksPresenter _libros;
        private string url { get; set; }
        public BooksController(IBooksPresenter libros)
        {
            _libros = libros;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _libros.Get());
        }

        [HttpGet]
        [Route("[controller]/{apa}")]
        public async Task<IActionResult> GetByApa(string apa)
        {
            var result = await _libros.GetByParams(apa, 0);

            return _libros.errorResponse.errors.Count() > 0 ? 
                BadRequest(_libros.errorResponse) : Ok(result);
        }

        [HttpGet]
        [Route("[controller]/{year}/years")]
        public async Task<IActionResult> GetByYears(int year)
        {
            var result = await _libros.GetByParams(string.Empty, year);

            return _libros.errorResponse.errors.Count() > 0 ?
                BadRequest(_libros.errorResponse) : Ok(result);
        }

        [HttpGet]
        [Route("[controller]/years")]
        public async Task<IActionResult> GetYears()
        {
            return Ok(await _libros.GetYears());
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Post(BooksPostData request)
        {
            url = Request.Scheme + "//" + Request.Host.Value + Request.Path.Value;
            var result = await _libros.Post(request);
            return _libros.errorResponse.errors.Count() > 0 ? BadRequest(_libros.errorResponse) : Created(url, result);
        }

        [HttpDelete]
        [Route("[controller]/{apa}")]
        public async Task<IActionResult> Delete(string apa)
        {
            url = Request.Scheme + "//" + Request.Host.Value + Request.Path.Value;
            var result = await _libros.Delete(apa);
            return _libros.errorResponse.errors.Count() > 0 ? BadRequest(_libros.errorResponse) : Created(url, result);
        }
    }
}
