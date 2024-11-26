using EAS_Hub.DbModels;

namespace EAS_Hub.ApiModels;

public class ModuleDevelop
{
    public Module Module { get; set; }
    public List<Position> Positions { get; set; }
}