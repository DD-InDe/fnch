namespace EAS_Hub.ApiModels;

public class NewQuestion
{
    public string Text { get; set; }
    public string Answer { get; set; }
    public int TypeId { get; set; }
    public int ModuleId { get; set; }
}