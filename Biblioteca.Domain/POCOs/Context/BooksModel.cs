
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Infrastructure.BDServices.Models
{
    public class BooksModel
    {
        public string? name { get; set; }
        public string? lastNames { get; set; }
        public string? title { get; set; }
        public int year { get; set; }
        public string? editorial { get; set; }
        public string? place { get; set; }
        public int availables { get; set; }

        [Key]
        public string? apa { get; set; }
    }
}
