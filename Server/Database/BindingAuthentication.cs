using System;
using System.Collections.Generic;

namespace OSIC.Server.Database;

public partial class BindingAuthentication
{
    public Guid Id { get; set; }

    public bool IsActive { get; set; }

    public bool IsVerified { get; set; }

    public Guid BindingId { get; set; }

    public virtual Binding Binding { get; set; } = null!;
}
