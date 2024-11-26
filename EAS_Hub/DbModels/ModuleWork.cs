using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EAS_Hub.DbModels;

public partial class ModuleWork
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? RoleId { get; set; }

    public bool? Main { get; set; }

    public bool? Accepted { get; set; }

    public int? ModuleId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Module? Module { get; set; }

    public virtual Role? Role { get; set; }
}
