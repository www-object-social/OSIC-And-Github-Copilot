using System;
using System.Collections.Generic;

namespace OSIC.Server.Database;

public partial class Binding
{
    public Guid Id { get; set; }

    public DateTime Expires { get; set; }

    public DateTime Created { get; set; }

    public int ProjectSoftware { get; set; }

    public int ProjectType { get; set; }

    public int UnitType { get; set; }

    public int UnitTwoLetterIsoregionName { get; set; }

    public int UnitTwoLetterIsolanguageName { get; set; }

    public int UnitTimeZone { get; set; }

    public virtual ICollection<BindingAuthentication> BindingAuthentications { get; } = new List<BindingAuthentication>();

    public virtual ICollection<BindingConnection> BindingConnections { get; } = new List<BindingConnection>();

    public virtual ICollection<BindingSecurity> BindingSecurities { get; } = new List<BindingSecurity>();
}
