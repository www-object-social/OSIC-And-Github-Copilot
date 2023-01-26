using System;
using System.Collections.Generic;

namespace OSIC.Server.Database;

public partial class BindingSecurity
{
    public Guid Id { get; set; }

    public Guid BindingId { get; set; }

    public DateTime Created { get; set; }

    public virtual Binding Binding { get; set; } = null!;
}
