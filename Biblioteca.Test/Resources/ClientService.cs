using System.Net.Http.Headers;
using System.Text;

namespace Biblioteca.Test.Resources
{
    public class ClientService
    {
        public HttpRequestMessage Get(string url, string header)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
            return request;
        }

        public HttpRequestMessage Post(string url, string header, string json, string method)
        {
            StringContent content = new StringContent(json, Encoding.UTF8, header);
            var request = new HttpRequestMessage(GetMethod(method), url)
            {
                Content = content
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
            return request;
        }

        public HttpRequestMessage Delete(string url, string header)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
            return request;
        }
        private HttpMethod GetMethod(string request)
        {
            switch (request.ToLower())
            {
                case "post": return HttpMethod.Post;
                case "patch": return HttpMethod.Patch;
                case "delete": return HttpMethod.Delete;
                default: return HttpMethod.Get;
            }
        }
    }
}
