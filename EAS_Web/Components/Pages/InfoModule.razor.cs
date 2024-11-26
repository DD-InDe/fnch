using Aspose.Words.Lists;
using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;
using EAS_Hub.Services;
using Microsoft.AspNetCore.Components;

namespace EAS_Web.Components.Pages;

public partial class InfoModule : ComponentBase
{

    #region Parameters

    [Parameter] public string Mode { get; set; }
    [Parameter] public int ModuleId { get; set; }

    private Module _module;
    private List<Position> _positions;
    private List<Event> _events;
    private List<Question> _questions;
    private List<Material> _materials;

    private string? _message = "";
    private bool _loaded;

    private string _why = "";
    private string _problems = "";
    private int _days;

    #endregion

    //todo сделать согласование / отклонение

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _loaded = false;
            ModuleDevelopInfo info = await FormationService.GetModuleDevelop(ModuleId, AuthService.Employee.Jwt);
            _module = info.Module;
            _positions = info.InlcudedPositions;
            _events = info.Events;
            _questions = info.Questions;
            _materials = info.Materials;
            _loaded = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _message = e.Message;
        }
    }

    private async Task RejectModule()
    {
        try
        {
            await FormationService.AccessModule(new ModuleAccept()
            {
                ModuleId = ModuleId,
                EmployeeId = AuthService.Employee.Id,
                IsAccepted = false
            }, AuthService.Employee.Jwt);
            Navigation.NavigateTo("/home");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    private async Task AccessModule()
    {
        try
        {
            await FormationService.AccessModule(new ModuleAccept()
            {
                ModuleId = ModuleId,
                EmployeeId = AuthService.Employee.Id,
                IsAccepted = true
            }, AuthService.Employee.Jwt);
            Navigation.NavigateTo("/home");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}