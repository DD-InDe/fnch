using EAS_Hub.DbModels;

namespace EAS_Hub.ApiModels;

public class ModuleForHr
{
    public Module? Module { get; set; }
    public List<Employee>? Developers { get; set; }
    public List<Employee>? Accessors { get; set; }
    public List<Position>? Positions { get; set; }
}