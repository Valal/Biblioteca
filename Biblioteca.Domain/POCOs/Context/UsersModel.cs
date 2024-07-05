using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Infrastructure.BDServices.Models
{
    public class UsersModel
    {
        public string? password { get; set; }
        public string? user { get; set; }
        public string? name { get; set; }
        public string? lastName { get; set; }
        [Key]
        public int idUser { get; set; }
    }
}
