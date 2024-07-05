
using Biblioteca.Domain.DTOs.Request.Data.Attributes;

namespace Biblioteca.Domain.DTOs.Request.Data
{
    public class UsersLoginData
    {
        public string? type { get; set; }
        public UsersLoginAttributes? attributes { get; set; }
    }
}
