using Biblioteca.Domain.DTOs.Response.Data.Attributes;

namespace Biblioteca.Domain.DTOs.Response.Data
{
    public class BooksDataSingle
    {
        public string? type { get; set; }
        public BooksAttributes? attributes {  get; set; }
    }
}
