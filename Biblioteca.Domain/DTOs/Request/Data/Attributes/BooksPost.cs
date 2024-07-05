namespace Biblioteca.Domain.DTOs.Request.Data.Attributes
{
    public class BooksPost
    {
        public string? name { get; set; }
        public string? lastNames { get; set; }
        public string? title { get; set; }
        public int year { get; set; }
        public string? place { get; set; }
        public string? editorial { get; set; }
        public int availables { get; set; }
    }
}
