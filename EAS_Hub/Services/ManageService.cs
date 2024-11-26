using System.Net.Http.Json;
using System.Text.Json;
using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;

namespace EAS_Hub.Services;

public class ManageService : ApiBase
{
    public static async Task<List<ModuleForHr>> GetModules()
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + "api/manage/modules");
            return message.IsSuccessStatusCode
                ? await JsonSerializer.DeserializeAsync<List<ModuleForHr>>(await message.Content.ReadAsStreamAsync(),
                    Options)
                : new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> AddModule(NewModule newModule)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + "api/manage/modules", newModule);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static async Task<bool> AddMap(NewAdaptationMap map)
    {
        try
        {
            var message = await Client.PostAsJsonAsync(BaseUrl + "api/manage/adaptationMaps", map);
            return message.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static async Task<List<EventAnalyze>> GetEventAnalyzes(int filterId = 0)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/manage/events/filterId={filterId}");
            return message.IsSuccessStatusCode
                ? await JsonSerializer.DeserializeAsync<List<EventAnalyze>>(await message.Content.ReadAsStreamAsync(),
                    Options)
                : new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static async Task<List<ModuleAnalyze>> GetModuleAnalyze(int filterId)
    {
        try
        {
            var message = await Client.GetAsync(BaseUrl + $"api/manage/results/filterId={filterId}");
            return message.IsSuccessStatusCode
                ? await JsonSerializer.DeserializeAsync<List<ModuleAnalyze>>(await message.Content.ReadAsStreamAsync(),
                    Options)
                : new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}