using System.Net;
using System.Text;
using System.Text.Json;

namespace Ecommerce_Customers_Site.Helpers
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            //if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //{
            //    throw new UnauthorizedAccessException("Token has expired");
            //}

            if (response.IsSuccessStatusCode == false)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"{errorMessage}");
            }
                //throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<T>(
                dataAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result;
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string url, T data)
        {
            var jsonContent = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return await client.PostAsync(url, content);
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string url, T data)
        {
            var jsonContent = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return await client.PutAsync(url, content);
        }
    }
}
