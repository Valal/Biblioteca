using Biblioteca.Domain.DTOs.Response.Data.Attributes;

namespace Biblioteca.Domain.DTOs.Response.Data
{
    public class BookYearsData
    {
        public string? type { get; set; }
        public List<BookYearsAttributes>? attributes { get; set; }
    }
}
