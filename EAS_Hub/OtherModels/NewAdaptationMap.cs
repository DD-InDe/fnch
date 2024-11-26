namespace EAS_Hub.ApiModels;

public class NewAdaptationMap
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int PositionId { get; set; }
    public int DepartmentId { get; set; }
    public List<NewMapModule> ModuleList { get; set; }
}