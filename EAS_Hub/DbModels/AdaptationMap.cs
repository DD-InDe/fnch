using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class AdaptationMap
{
    public int Id { get; set; }

    public DateTime? DateCreate { get; set; }

    public bool? Hired { get; set; }

    public int? EmployeeId { get; set; }

    public int? PositionId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Employee? Employee { get; set; }

    [JsonIgnore] public virtual ICollection<MapModule> MapModules { get; set; } = new List<MapModule>();

    public virtual Position? Position { get; set; }

    [JsonIgnore] public virtual ICollection<TestingResult> TestingResults { get; set; } = new List<TestingResult>();
}
