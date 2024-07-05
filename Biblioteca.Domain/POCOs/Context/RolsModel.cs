using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Infrastructure.BDServices.Models
{
    public class RolsModel
    {
        [Key]
        public int idRole { get; set; }
        public string? roleName { get; set; }
        public int ? role { get; set; }
    }
}
