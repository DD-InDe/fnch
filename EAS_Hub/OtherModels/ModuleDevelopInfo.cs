using EAS_Hub.DbModels;

namespace EAS_Hub.ApiModels;

public class ModuleDevelopInfo
{
    public Module Module { get; set; }
    public List<Position> InlcudedPositions { get; set; }
    public List<Event> Events { get; set; }
    public List<Material> Materials { get; set; }
    public List<Question> Questions { get; set; }
}