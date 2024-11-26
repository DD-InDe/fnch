using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;
using EAS_Hub.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace EAS_Web.Components.Pages;

public partial class DevelopModule : ComponentBase
{
    #region Parameters

    [Parameter] public int ModuleId { get; set; }
    private Module _module;

    private List<EventType> _eventTypes;
    private List<Employee> _employees;
    private List<Module> _modules;
    private List<Event> _events;
    private List<Question> _questions;
    private List<Material> _materials;
    private List<TestingType> _testingTypes;

    private bool _loaded;
    private List<WebModuleAccess> _accesses;

    private Event _event;
    private NewQuestion _newQuestion;
    private Material _material;

    private string _message;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if(AuthService.Employee == null) Navigation.NavigateTo("/");
            _loaded = false;
            await LoadData();
            _loaded = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _message = e.Message;
        }
    }

    private async Task SaveData()
    {
        try
        {
            ModuleDevelop moduleDevelop = new()
            {
                Module = _module,
                Positions = _accesses
                    .Where(c => c.IsChecked)
                    .Select(c => c.Position)
                    .ToList()
            };

            await FormationService.UpdateModule(moduleDevelop, AuthService.Employee.Jwt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _message = e.Message;
        }
    }

    private async Task SendToAccess()
    {
        try
        {
            await FormationService.ModuleToAccess(_module.Id, AuthService.Employee.Jwt);
            Navigation.NavigateTo("/home");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #region AddMethods

    private async Task AddEvent()
    {
        try
        {
            await FormationService.AddEvent(_event, AuthService.Employee.Jwt);
            _event = new()
            {
                ModuleId = ModuleId
            };
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _message = e.Message;
        }
    }

    private async Task AddMaterial()
    {
        try
        {
            if (_material.Url != null || _material.Data != null)
            {
                await FormationService.AddMaterial(_material, AuthService.Employee.Jwt);
                _material = new()
                {
                    ModuleId = ModuleId
                };
                await LoadData();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _message = e.Message;
        }
    }

    private async Task AddQuestion()
    {
        try
        {
            await FormationService.AddQuestion(_newQuestion, AuthService.Employee.Jwt);
            _newQuestion = new()
            {
                ModuleId = ModuleId
            };
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _message = e.Message;
        }
    }

    private async void LoadFile(InputFileChangeEventArgs obj)
    {
        try
        {
            long max = 20000000;
            if (obj.File.Size <= max)
            {
                var memory = new MemoryStream();
                Stream stream = obj.File.OpenReadStream(max);
                await stream.CopyToAsync(memory);
                stream.Close();
                _material.FileName = obj.File.Name;
                _material.Data = memory.ToArray();
            }
            else
                _message = "Файл слишком большой";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    #endregion

    async Task LoadData()
    {
        try
        {
            ModuleDevelopInfo moduleDevelopInfo = await FormationService.GetModuleDevelop(ModuleId, AuthService.Employee.Jwt);
            _module = moduleDevelopInfo.Module;
            _module.LastChangeById = AuthService.Employee.Id;
            _materials = moduleDevelopInfo.Materials;
            _events = moduleDevelopInfo.Events;
            _questions = moduleDevelopInfo.Questions;

            _eventTypes = await Db.Context.EventTypes.ToListAsync();
            _employees = await Db.Context.Employees.ToListAsync();
            _modules = await Db.Context.Modules.ToListAsync();
            _testingTypes = await Db.Context.TestingTypes.ToListAsync();

            _accesses = new();
            foreach (var position in await Db.Context.Positions.ToListAsync())
            {
                _accesses.Add(new()
                {
                    Position = position,
                    IsChecked = moduleDevelopInfo.InlcudedPositions.Exists(c => c.Id == position.Id)
                });
            }

            _event = new() { ModuleId = ModuleId };
            _newQuestion = new() { ModuleId = ModuleId };
            _material = new() { ModuleId = ModuleId };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _message = e.Message;
        }
    }
}