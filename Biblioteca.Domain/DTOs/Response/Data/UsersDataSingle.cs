
using Biblioteca.Domain.DTOs.Response.Data.Attributes;

namespace Biblioteca.Domain.DTOs.Response.Data
{
    public class UsersDataSingle
    {
        public string? type { get; set; }
        public UsersAttributes? attributes { get; set; }
    }
}
