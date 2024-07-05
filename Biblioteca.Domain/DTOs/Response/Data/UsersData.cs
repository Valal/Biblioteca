using Biblioteca.Domain.DTOs.Response.Data.Attributes;

namespace Biblioteca.Domain.DTOs.Response.Data
{
    public class UsersData
    {
        public string? type { get; set; }
        public List<UsersAttributes>? attributes { get; set; }
    }
}
