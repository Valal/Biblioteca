using Biblioteca.Domain.DTOs.Response.Data.Attributes;

namespace Biblioteca.Domain.DTOs.Response.Data
{
    public class UsersBooksData
    {
        public string? type { get; set; }
        public List<BooksByUserAttributes>? attributes { get; set; }
    }
}
