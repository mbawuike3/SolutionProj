using System.Text;
using System.Text.Json;

namespace ProjectKpi.Helpers;

public interface IHttpConnection
{
    Task<(T? result, object? error)> WebConnection<T>(string url, string requestType, object? payload = null, Dictionary<string, string>? headers = null, string? authUser = null, string? authPaswrd = null) where T : new();
}

public class HttpConnection : IHttpConnection
{
    public async Task<(T? result, object? error)> WebConnection<T>(string url, string requestType, object? payload = null,
        Dictionary<string, string>? headers = null, string? authUser = null, string? authPaswrd = null) where T : new()
    {
        T? result = new();
        object? error = null;
        try
        {
            using var client = new HttpClient();
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
            };
            switch (requestType.ToLower())
            {
                case "post":
                    requestMessage.Method = HttpMethod.Post;
                    break;
                case "get":
                    requestMessage.Method = HttpMethod.Get;
                    break;
                case "delete":
                    requestMessage.Method = HttpMethod.Delete;
                    break;
                case "put":
                    requestMessage.Method = HttpMethod.Put;
                    break;
                default:
                    throw new ArgumentException("Invalid request type");
            }
            if (payload != null)
            {
                var jsonPayload = JsonSerializer.Serialize(payload);
                requestMessage.Content = new StringContent(jsonPayload, encoding: Encoding.UTF8, "application/json");
            }
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }
            if (authUser != null && authPaswrd != null)
            {
                var byteArray = Encoding.ASCII.GetBytes($"{authUser}:{authPaswrd}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                result = JsonSerializer.Deserialize<T>(responseContent);
            }
            else
            {
                error = new {errorCode = response.StatusCode, errorMessage = responseContent };
            }
        }
        catch (Exception ex)
        {
            error = new { errorCode = "Exception", errorMessage = ex.Message };
        }
        return (result, error);
    }
}
