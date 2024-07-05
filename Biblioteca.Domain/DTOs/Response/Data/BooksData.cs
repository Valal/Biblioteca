
using Biblioteca.Domain.DTOs.Response.Data.Attributes;

namespace Biblioteca.Domain.DTOs.Response.Data
{
    public class BooksData
    {
        public string? type { get; set; }
        public List<BooksAttributes>? attributes { get; set; }
    }
}
