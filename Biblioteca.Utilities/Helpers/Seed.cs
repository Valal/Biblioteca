using System.Reflection;
using System.Text.Json;
using static Biblioteca.Utilities.Helpers.Base;

namespace Biblioteca.Utilities.Helpers
{
    public static class Seed
    {
        public static List<TEntity> SeedData<TEntity>(string fileName, string path, bool customPath = false)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            
            if (customPath)
                currentDirectory = GetRootPath();

            string fullPath = Path.Combine(currentDirectory, path, fileName);

            using (StreamReader reader = new StreamReader(fullPath))
            {
                string json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<List<TEntity>>(json);
            }
        }
    }
}
