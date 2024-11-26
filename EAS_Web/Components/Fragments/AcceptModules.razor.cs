using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;
using EAS_Hub.Services;
using Microsoft.AspNetCore.Components;

namespace EAS_Web.Components.Fragments;

public partial class AcceptModules : ComponentBase
{
    private List<ModuleForAccessor> modules;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (AuthService.Employee != null)
                modules = await FormationService.GetAccessModules(AuthService.Employee.Id, AuthService.Employee.Jwt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private string StatusColor(Module module)
    {
        if ((module.DateCreate.Value.AddDays(module.DevelopDeadline.Value) - DateTime.Now).Days < 7
            && module.StatusId is 1 or 2)
            return "#e50000";

        return module.StatusId switch
        {
            1 => "#26b050",
            2 => "#f5fa64",
            _ => "white"
        };
    }
}