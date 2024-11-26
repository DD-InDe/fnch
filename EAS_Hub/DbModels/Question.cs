using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class Question
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public string? Answer { get; set; }

    public int? TestingId { get; set; }

    public virtual Testing? Testing { get; set; }
}
