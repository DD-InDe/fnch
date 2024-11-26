using System.Text.Json;

namespace EAS_Hub.Services;

public class ApiBase
{
    protected static string BaseUrl = "http://localhost:5169/";
    protected static HttpClient Client = new();
    protected static JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };
}