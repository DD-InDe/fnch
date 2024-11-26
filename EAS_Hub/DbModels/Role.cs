using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [JsonIgnore] public virtual ICollection<ModuleWork> ModuleWorks { get; set; } = new List<ModuleWork>();
}
