namespace EAS_Hub.ApiModels;

public class ModuleAccept
{
    public int ModuleId { get; set; }
    public int EmployeeId { get; set; }
    public bool IsAccepted { get; set; }
}