using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class EventType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [JsonIgnore] public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
