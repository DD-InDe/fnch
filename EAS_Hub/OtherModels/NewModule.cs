using EAS_Hub.DbModels;

namespace EAS_Hub.ApiModels;

public class NewModule
{
    public string Name { get; set; }
    public int Deadline { get; set; }
    public List<Employee> Developers { get; set; }
    public List<Employee> Accessors { get; set; }
    public Employee Main { get; set; }
}