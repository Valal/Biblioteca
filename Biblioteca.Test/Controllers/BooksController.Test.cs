
using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.DTOs.Request.Data.Attributes;
using Biblioteca.Test.Resources;
using System.Text.Json;

namespace Biblioteca.Test.Controllers
{
    public class BooksController : IClassFixture<WebAppFactoryBiblioteca<Program>>
    {
        private readonly ClientService _clientService;
        private readonly HttpClient httpClient;

        public BooksController(WebAppFactoryBiblioteca<Program> factory)
        {
            _clientService = new ClientService();
            httpClient = factory.CreateClient();
        }
        [Fact]
        public async Task Get_Books_Ok()
        {
            HttpRequestMessage request = _clientService.Get("books", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(200, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("Tolkien, (1954)")]
        public async Task Get_Books_By_Apa_Ok(string apa)
        {
            HttpRequestMessage request = _clientService.Get($"books/{apa}", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(200, (int)response.StatusCode);
        }
        [Theory]
        [InlineData("Rasputin, (1944)")]
        public async Task Get_Books_By_Apa_BadRequest(string apa)
        {
            HttpRequestMessage request = _clientService.Get($"books/{apa}", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(400, (int)response.StatusCode);
        }

        [Theory]
        [InlineData(2003)]
        public async Task Get_Books_By_Year_Ok(int year)
        {
            HttpRequestMessage request = _clientService.Get($"books/{year}/years", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(200, (int)response.StatusCode);
        }
        
        [Theory]
        [InlineData(5000)]
        public async Task Get_Books_By_Year_BadRequest(int year)
        {
            HttpRequestMessage request = _clientService.Get($"books/{year}/years", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(400, (int)response.StatusCode);
        }

        [Fact]
        public async Task Get_Years_Ok()
        {
            HttpRequestMessage request = _clientService.Get($"books/years", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(200, (int)response.StatusCode);
        }

        [Fact]
        public async Task Post_Books_Created()
        {
            var postData = new BooksPostData()
            {
                type = "libros",
                data = new BooksPost()
                {
                    availables = 3,
                    editorial = "Saragoza",
                    lastNames = "Silvano",
                    name = "Ruperto",
                    place = "Durango, México",
                    title = "Un suspiro de la mente",
                    year = 2024
                }
            };
            string jsonRequest = JsonSerializer.Serialize(postData);
            HttpRequestMessage request = _clientService.Post($"books/", "application/json", jsonRequest, "post");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(201, (int)response.StatusCode);
        }

        [Fact]
        public async Task Post_Books_RequestNotValid()
        {
            var postData = new BooksPostData()
            {
                type = "libros",
                data = new BooksPost()
            };
            string jsonRequest = JsonSerializer.Serialize(postData);
            HttpRequestMessage request = _clientService.Post($"books/", "application/json", jsonRequest, "post");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(400, (int)response.StatusCode);
        }

        [Fact]
        public async Task Post_Books_Exist()
        {
            var postData = new BooksPostData()
            {
                type = "libros",
                data = new BooksPost()
                {
                    name = "George",
                    lastNames = "R. R. Martin",
                    title = "Game of thrones",
                    year = 1996,
                    editorial = "Chronicle",
                    place = "California, E.U.",
                    availables = 2
                }
            };
            string jsonRequest = JsonSerializer.Serialize(postData);
            HttpRequestMessage request = _clientService.Post($"books/", "application/json", jsonRequest, "post");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(400, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("Tolkien, (1954)")]
        public async Task Delete_Books_Created(string apa)
        {
            HttpRequestMessage request = _clientService.Delete($"books/{apa}", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(201, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("Tzun Tzu, (1900)")]
        public async Task Delete_NotExists_Books_BadRequest(string apa)
        {
            HttpRequestMessage request = _clientService.Delete($"books/{apa}", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(400, (int)response.StatusCode);
        }
    }
}
