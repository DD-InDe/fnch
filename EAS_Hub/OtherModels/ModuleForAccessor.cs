using EAS_Hub.DbModels;

namespace EAS_Hub.ApiModels;

public class ModuleForAccessor
{
    public Module Module { get; set; }
    public List<Employee> Developers { get; set; }
}