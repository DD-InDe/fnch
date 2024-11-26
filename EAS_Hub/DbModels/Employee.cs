using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class Employee
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public string? Login { get; set; }
    public string? Jwt { get; set; }

    public string? Password { get; set; }

    public int? PositionId { get; set; }

    public int? DepartmentId { get; set; }

    [JsonIgnore] public virtual ICollection<AdaptationMap> AdaptationMaps { get; set; } = new List<AdaptationMap>();

    public virtual Department? Department { get; set; }

    [JsonIgnore] public virtual ICollection<Module> ModuleLastChangeBies { get; set; } = new List<Module>();

    [JsonIgnore] public virtual ICollection<Module> ModuleResponsibleEmployees { get; set; } = new List<Module>();

    [JsonIgnore] public virtual ICollection<ModuleWork> ModuleWorks { get; set; } = new List<ModuleWork>();

    public virtual Position? Position { get; set; }
}