using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;

namespace EAS_Hub.Services;

public class FormationService : ApiBase
{
    public static async Task<List<Module>> GetDevModules(int employeeId, string token)
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = await Client.GetAsync(BaseUrl + $"api/formation/modules/develop/employeeId={employeeId}");
            if (message.StatusCode == HttpStatusCode.Forbidden) throw new UnauthorizedAccessException();
            return message.IsSuccessStatusCode
                ? await JsonSerializer.DeserializeAsync<List<Module>>(await message.Content.ReadAsStreamAsync(),
                    Options)
                : new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<List<ModuleForAccessor>> GetAccessModules(int employeeId, string token)
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = await Client.GetAsync(BaseUrl + $"api/formation/modules/access/employeeId={employeeId}");
            if (message.StatusCode == HttpStatusCode.Forbidden) throw new UnauthorizedAccessException();
            return message.IsSuccessStatusCode
                ? await JsonSerializer.DeserializeAsync<List<ModuleForAccessor>>(
                    await message.Content.ReadAsStreamAsync(),
                    Options)
                : new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<ModuleDevelopInfo> GetModuleDevelop(int moduleId, string token)
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = await Client.GetAsync(BaseUrl + $"api/formation/modules/develop/moduleId={moduleId}");
            if (message.StatusCode == HttpStatusCode.Forbidden) throw new UnauthorizedAccessException();
            return message.IsSuccessStatusCode
                ? await JsonSerializer.DeserializeAsync<ModuleDevelopInfo>(
                    await message.Content.ReadAsStreamAsync(),
                    Options)
                : new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> UpdateModule(ModuleDevelop develop, string token)
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = await Client.PutAsJsonAsync(BaseUrl + "api/formation/modules/develop", develop);
            if (message.StatusCode == HttpStatusCode.Forbidden) throw new UnauthorizedAccessException();
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AddEvent(Event @event, string token)
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = await Client.PostAsJsonAsync(BaseUrl + "api/formation/modules/develop/events", @event);
            if (message.StatusCode == HttpStatusCode.Forbidden) throw new UnauthorizedAccessException();
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AddMaterial(Material material, string token)
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = await Client.PostAsJsonAsync(BaseUrl + "api/formation/modules/develop/materials", material);
            if (message.StatusCode == HttpStatusCode.Forbidden) throw new UnauthorizedAccessException();
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AddQuestion(NewQuestion question, string token)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + "api/formation/modules/develop/questions", question);
            if (message.StatusCode == HttpStatusCode.Forbidden) throw new UnauthorizedAccessException();
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> ModuleToAccess(int moduleId, string token)
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = await Client.GetAsync(BaseUrl + $"api/formation/modules/toAccess/moduleId={moduleId}");
            if (message.StatusCode == HttpStatusCode.Forbidden) throw new UnauthorizedAccessException();
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AccessModule(ModuleAccept accept, string token)
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = await Client.PostAsJsonAsync(BaseUrl + $"api/formation/modules/access", accept);
            if (message.StatusCode == HttpStatusCode.Forbidden) throw new UnauthorizedAccessException();
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}