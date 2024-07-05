using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain.POCOs.Context
{
    public class BooksByUserModel
    {
        [Key]
        public int idBookByUser { get; set; }
        public int idUser { get; set; }
        public string? apa { get; set; }
        public string? loanDate { get; set; }
    }
}
