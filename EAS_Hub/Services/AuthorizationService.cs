using System.Text.Json;
using EAS_Hub.DbModels;

namespace EAS_Hub.Services;

public class AuthorizationService : ApiBase
{
    public static async Task<Employee?> LogIn(string login, string password)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/session/login={login},password={password}");
            return message.IsSuccessStatusCode
                    ? await JsonSerializer.DeserializeAsync<Employee>(await message.Content.ReadAsStreamAsync(),
                        Options)
                    : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}