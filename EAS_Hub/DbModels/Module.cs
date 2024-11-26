using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class Module
{
    public int Id { get; set; }

    public string? CodeName { get; set; }

    public string? Name { get; set; }
    
    public string? Source { get; set; }

    public DateTime? DateCreate { get; set; }

    public int? DevelopDeadline { get; set; }

    public int? CompleteDeadline { get; set; }

    public int? LastChangeById { get; set; }

    public int? PreviousId { get; set; }

    public int? ResponsibleEmployeeId { get; set; }

    public int? StatusId { get; set; }

    [JsonIgnore] public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [JsonIgnore] public virtual ICollection<Module> InversePrevious { get; set; } = new List<Module>();

    public virtual Employee? LastChangeBy { get; set; }

    [JsonIgnore] public virtual ICollection<MapModule> MapModules { get; set; } = new List<MapModule>();

    [JsonIgnore] public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    [JsonIgnore] public virtual ICollection<ModuleAccess> ModuleAccesses { get; set; } = new List<ModuleAccess>();

    [JsonIgnore] public virtual ICollection<ModuleWork> ModuleWorks { get; set; } = new List<ModuleWork>();

    public virtual Module? Previous { get; set; }

    public virtual Employee? ResponsibleEmployee { get; set; }

    public virtual ModuleStatus? Status { get; set; }

    [JsonIgnore] public virtual ICollection<Testing> Testings { get; set; } = new List<Testing>();
}