using Aspose.Words.Lists;
using EAS_Hub.DbModels;
using EAS_Hub.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace EAS_Web.Components.Fragments;

public partial class DevelopModules : ComponentBase
{
    private int statusId;
    private string search = String.Empty;
    private bool archive = false;

    private List<ModuleStatus> statusList;
    private List<Module> modules;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if(AuthService.Employee == null) Navigation.NavigateTo("/");
            
            statusList = await Db.Context.ModuleStatuses.ToListAsync();
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task LoadData()
    {
        try
        {
            if (AuthService.Employee != null)
                modules = await FormationService.GetDevModules(AuthService.Employee.Id, AuthService.Employee.Jwt);
            modules = modules
                .Where(c =>
                {
                    if (c.Name != null)
                        return
                            c
                                .Name.ToLower()
                                .Contains(search.ToLower());
                    return true;
                })
                .ToList();
            if (!archive)
                modules = modules
                    .Where(c => c.StatusId != 4)
                    .ToList();
            if (statusId != 0)
                modules = modules
                    .Where(c => c.StatusId == statusId)
                    .ToList();
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

    #region SearchAndFilter

    private async Task ChangeCheck(ChangeEventArgs obj)
    {
        try
        {
            archive = (bool)obj.Value;
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task ChangeStatus(ChangeEventArgs obj)
    {
        try
        {
            statusId = Convert.ToInt32(obj.Value);
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task ChangeSearch(ChangeEventArgs obj)
    {
        try
        {
            search = (string)obj.Value;
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    #endregion
}