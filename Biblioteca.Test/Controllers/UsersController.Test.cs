using Biblioteca.Domain.DTOs.Request.Data;
using Biblioteca.Domain.DTOs.Request.Data.Attributes;
using Biblioteca.Test.Resources;
using System.Text.Json;

namespace Biblioteca.Test.Controllers
{
    public class UsersController : IClassFixture<WebAppFactoryBiblioteca<Program>>
    {
        private readonly ClientService _clientService;
        private readonly HttpClient httpClient;

        public UsersController(WebAppFactoryBiblioteca<Program> factory)
        {
            _clientService = new ClientService();
            httpClient = factory.CreateClient();
        }

        [Theory]
        [InlineData("juca", "campaj50")]
        public async Task User_Login_Post_Success(string user, string pwd)
        {
            var loginPost = new UsersLoginData()
            {
                type = "usuario",
                attributes = new UsersLoginAttributes()
                {
                    usuario = user,
                    password = pwd
                }
            };
            string jsonRequest = JsonSerializer.Serialize(loginPost);
            HttpRequestMessage request = _clientService.Post("users/login", "application/json", jsonRequest, "post");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(200, (int)response.StatusCode);
        }

        [Fact]
        public async Task User_Login_Post_Empty()
        {
            var loginPost = new UsersLoginData()
            {
                type = "usuario",
                attributes = new UsersLoginAttributes()
            };
            string jsonRequest = JsonSerializer.Serialize(loginPost);
            HttpRequestMessage request = _clientService.Post("users/login", "application/json", jsonRequest, "post");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(400, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("carmain", "kdetalle")]
        public async Task User_Login_Post_NotExists(string user, string pwd)
        {
            var loginPost = new UsersLoginData()
            {
                type = "usuario",
                attributes = new UsersLoginAttributes()
                {
                    usuario = user,
                    password = pwd
                }
            };
            string jsonRequest = JsonSerializer.Serialize(loginPost);
            HttpRequestMessage request = _clientService.Post("users/login", "application/json", jsonRequest, "post");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(400, (int)response.StatusCode);
        }

        [Fact]
        public async Task User_Login_Get_Ok()
        {
            HttpRequestMessage request = _clientService.Get("users", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(200, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("juca")]
        public async Task User_Get_ByUser_Ok(string user)
        {
            HttpRequestMessage request = _clientService.Get($"users/{user}", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(200, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("pabloM")]
        public async Task User_BooksByUser_Ok(string user)
        {
            HttpRequestMessage request = _clientService.Get($"users/{user}/books", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(200, (int)response.StatusCode);
        }
        
        [Theory]
        [InlineData("juca")]
        public async Task User_BooksByUser_Fail(string user)
        {
            HttpRequestMessage request = _clientService.Get($"users/{user}/books", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(400, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("pabloM", "Druon, (1960)")]
        public async Task User_BooksRequestByUser_Ok(string user, string apa)
        {
            HttpRequestMessage request = _clientService.Get($"users/{user}/books/{apa}/request", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(200, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("pabloM", "Fisher, (1987)")]
        public async Task User_BooksPriorRequest_Fail(string user, string apa)
        {
            HttpRequestMessage request = _clientService.Get($"users/{user}/books/{apa}/request", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(400, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("juca", "Fisher, (1987)")]
        public async Task User_Books_NoAvailable(string user, string apa)
        {
            HttpRequestMessage request = _clientService.Get($"users/{user}/books/{apa}/request", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(400, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("pabloM", "Fisher, (1987)")]
        public async Task User_Books_Delivery(string user, string apa)
        {
            HttpRequestMessage request = _clientService.Get($"users/{user}/books/{apa}/deliver", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(200, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("pabloM")]
        public async Task User_Permissions_Ok(string user)
        {
            HttpRequestMessage request = _clientService.Get($"users/{user}/permissions", "application/json");
            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(200, (int)response.StatusCode);
        }
    }
}
