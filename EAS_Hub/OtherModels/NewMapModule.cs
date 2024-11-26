using System.Text.Json.Serialization;
using EAS_Hub.DbModels;

namespace EAS_Hub.ApiModels;

public class NewMapModule
{
    public Module Module { get; set; }
    public Employee? Employee { get; set; }
    public bool IsChecked { get; set; }
    [JsonIgnore] public List<Employee>? Employees { get; set; }
}