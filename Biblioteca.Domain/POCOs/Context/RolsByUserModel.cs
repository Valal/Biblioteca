using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Infrastructure.BDServices.Models
{
    public class RolsByUserModel
    {
        [Key]
        public int idRolsByUser { get; set; }
        public int idUser { get; set; }
        public int role { get; set; }
    }
}
