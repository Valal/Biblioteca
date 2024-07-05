
using Biblioteca.Domain.DTOs.Request.Data.Attributes;

namespace Biblioteca.Domain.DTOs.Request.Data
{
    public class BooksPostData
    {
        public string? type { get; set; }
        public BooksPost? data { get; set; }
    }
}
