using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class Material
{
    public int Id { get; set; }

    public string? MaterialName { get; set; }

    public string? FileName { get; set; }
    
    public string? Url { get; set; }

    public byte[]? Data { get; set; }

    public int? ModuleId { get; set; }

    public virtual Module? Module { get; set; }
}
