using System;
using System.Collections.Generic;

namespace OSIC.Server.Database;

public partial class BindingConnection
{
    public Guid Id { get; set; }

    public Guid BindingId { get; set; }

    public string SignalrConnectionId { get; set; } = null!;

    public int ProjectSoftware { get; set; }

    public string MachineName { get; set; } = null!;

    public bool IsDeveloper { get; set; }

    public virtual Binding Binding { get; set; } = null!;
}
