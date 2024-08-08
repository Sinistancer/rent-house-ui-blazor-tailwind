using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace daryon_house_ui.Server.Applications
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string url, string? token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token?.ToString());

                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (HttpRequestException httpRequestException)
            {
                // Handle HTTP request specific exceptions here.
                // Log the exception or rethrow with additional information if necessary.
                // throw new Exception($"Error fetching data from {url}. HTTP Request Exception: {httpRequestException.Message}", httpRequestException);
                return default;
            }
            catch (NotSupportedException notSupportedException)
            {
                // Handle cases where the content is not a valid JSON format.
                // throw new Exception($"The content at {url} is not in a supported format. Not Supported Exception: {notSupportedException.Message}", notSupportedException);
                return default;
            }
            catch (Newtonsoft.Json.JsonException jsonException)
            {
                // Handle cases where the JSON format is incorrect or not expected.
                // throw new Exception($"Error parsing JSON response from {url}. JSON Exception: {jsonException.Message}", jsonException);
                return default;
            }
            catch (Exception exception)
            {
                // Handle any other exceptions that may occur.
                // throw new Exception($"An error occurred while fetching data from {url}. Exception: {exception.Message}", exception);
                return default;
            }
        }

        public async Task<T> PostAsync<T, TRequest>(string url, TRequest requestBody, string? token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = System.Text.Json.JsonSerializer.Serialize(requestBody, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var test = await response.Content.ReadAsStringAsync();

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (HttpRequestException httpRequestException)
            {
                // Handle HTTP request specific exceptions here.
                // Log the exception or rethrow with additional information if necessary.
                // throw new Exception($"Error fetching data from {url}. HTTP Request Exception: {httpRequestException.Message}", httpRequestException);
                return default;
            }
            catch (NotSupportedException notSupportedException)
            {
                // Handle cases where the content is not a valid JSON format.
                // throw new Exception($"The content at {url} is not in a supported format. Not Supported Exception: {notSupportedException.Message}", notSupportedException);
                return default;
            }
            catch (Newtonsoft.Json.JsonException jsonException)
            {
                // Handle cases where the JSON format is incorrect or not expected.
                // throw new Exception($"Error parsing JSON response from {url}. JSON Exception: {jsonException.Message}", jsonException);
                return default;
            }
            catch (Exception exception)
            {
                // Handle any other exceptions that may occur.
                // throw new Exception($"An error occurred while fetching data from {url}. Exception: {exception.Message}", exception);
                return default;
            }
        }
    }
}
