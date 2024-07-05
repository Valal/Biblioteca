using Biblioteca.Domain.DTOs.Response.Data;

namespace Biblioteca.Domain.DTOs.Response
{
    public class ErrorResponse
    {
        public List<Errors> errors { get; set; }
    }
}
