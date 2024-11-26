using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class Position
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [JsonIgnore] public virtual ICollection<AdaptationMap> AdaptationMaps { get; set; } = new List<AdaptationMap>();

    [JsonIgnore] public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [JsonIgnore] public virtual ICollection<ModuleAccess> ModuleAccesses { get; set; } = new List<ModuleAccess>();
}
