using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class TestingResult
{
    public int Id { get; set; }

    public int? MapId { get; set; }

    public int? TestingId { get; set; }

    public int? Correct { get; set; }

    public int? Error { get; set; }

    public virtual AdaptationMap? Map { get; set; }

    public virtual Testing? Testing { get; set; }
}
