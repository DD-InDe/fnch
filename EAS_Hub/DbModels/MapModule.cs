using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class MapModule
{
    public int Id { get; set; }

    public int? MapId { get; set; }

    public int? ModuleId { get; set; }

    public bool? IsComplete { get; set; }

    public virtual AdaptationMap? Map { get; set; }

    public virtual Module? Module { get; set; }
}
