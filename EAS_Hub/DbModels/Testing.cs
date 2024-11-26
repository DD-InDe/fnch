using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class Testing
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public int? ModuleId { get; set; }

    public virtual Module? Module { get; set; }

    [JsonIgnore] public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [JsonIgnore] public virtual ICollection<TestingResult> TestingResults { get; set; } = new List<TestingResult>();

    public virtual TestingType? Type { get; set; }
}
