using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OSIC.Server.Hosting.server;
using OSIC.Shared.Project;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
namespace OSIC.Server.Hosting.gateway;
public partial class Binding : Hub
{
    private Database.BindingConnection DBBindingConnection {
        get {
            using var DB = this.DBContextFactory.CreateDbContext();
            var D = DB.BindingConnections.Single(x => x.SignalrConnectionId == Context.ConnectionId&& x.MachineName==System.Environment.MachineName);
            if (D.Binding.Expires < DateTime.UtcNow.AddDays(2)) {
                D.Binding.Expires = DateTime.UtcNow.AddDays(3);
                DB.SaveChanges();
            }
            return D;
        }
    }
    public Guid ID => DBBindingConnection.BindingId;
    private Database.Binding DBBinding {
        get {
            using var DB = this.DBContextFactory.CreateDbContext();
            return DB.Bindings.Single(x => x.Id == this.ID);
        }
    }
    public int ConnectionsCount => DBBinding.BindingConnections.Count;
    public (string MachineName, Software Software, bool IsDeveloper, string ConnectionID)[] Connections => DBBinding.BindingConnections.Select(x => (x.MachineName, (Software)x.ProjectSoftware, x.IsDeveloper, x.SignalrConnectionId)).ToArray();
    #region Handler Binding
    public Task<OSIC.Shared.Response.binding.Data> Create(OSIC.Shared.Project.Software ProjectSoftware, OSIC.Shared.Project.Type ProjectType, OSIC.Shared.Unit.Type UnitType, string UnitTwoLetterISORegionName, string UnitTwoLetterISOLanguageName, double UnitTimeZone)
    {
        using var DB = DBContextFactory.CreateDbContext();
        OSIC.Shared.Project.Region R;
        if (!Enum.TryParse(UnitTwoLetterISORegionName, true, out R))
            R = Shared.Project.Region.DK;
        OSIC.Shared.Project.Language L;
        if (!Enum.TryParse(UnitTwoLetterISOLanguageName, true, out L))
            L = Shared.Project.Language.EN;
        var B = new OSIC.Server.Database.Binding { Id = Guid.NewGuid(), Created = DateTime.UtcNow, Expires = DateTime.UtcNow.AddDays(3), UnitTimeZone = (int)UnitTimeZone, ProjectSoftware = (int)ProjectSoftware, ProjectType = (int)ProjectType, UnitType = (int)UnitType, UnitTwoLetterIsolanguageName = (int)L, UnitTwoLetterIsoregionName = (int)R };
        var S = new Database.BindingSecurity { Id = Guid.NewGuid(), Created = DateTime.UtcNow };
        B.BindingSecurities.Add(S);
        B.BindingConnections.Add(new Database.BindingConnection
        {
            Id = Guid.NewGuid(),
            MachineName = System.Environment.MachineName,
            ProjectSoftware = (int)ProjectSoftware,
            SignalrConnectionId = Context.ConnectionId,
            IsDeveloper =
#if DEBUG
                true
#else
            false
#endif
        });
        DB.Bindings.Add(B);
        DB.SaveChanges();
        return Task.FromResult(new OSIC.Shared.Response.binding.Data { Key = ($"{B.Id.ToString()}:{S.Id.ToString()}").Encrypt(), Language = L, Region = R, Storage = Shared.Certificate.Storage.Temporary });
    }
    public async Task<OSIC.Shared.Response.binding.Data> Validation(OSIC.Shared.Project.Software ProjectSoftware, OSIC.Shared.Project.Type ProjectType, OSIC.Shared.Unit.Type UnitType, string UnitTwoLetterISORegionName, string UnitTwoLetterISOLanguageName, double UnitTimeZone, string Key)
    {
        using var DB = DBContextFactory.CreateDbContext();
        try
        {
            Guid BindingID, BindingSecurityID;
            var D = Key.Decrypt();
            if (D.IndexOf(":") == -1 || !Guid.TryParse(D.Split(":").First(), out BindingID) || !Guid.TryParse(D.Split(":").Last(), out BindingSecurityID) || !DB.BindingSecurities.Any(x => x.BindingId == BindingID && x.Id == BindingSecurityID)) throw new Exception();
            var B = DB.Bindings.Single(x => x.Id == BindingID);
            B.UnitTimeZone = (int)UnitTimeZone;
            if (B.Expires < DateTime.UtcNow.AddDays(2))
                B.Expires = DateTime.UtcNow.AddDays(3);
            var S = new Database.BindingSecurity { Id = Guid.NewGuid(), Created = DateTime.UtcNow };
            B.BindingSecurities.Add(S);
            Shared.Certificate.Storage SCS = B.BindingAuthentications.Any(x => x.IsActive || x.IsVerified) ? Shared.Certificate.Storage.Local : Shared.Certificate.Storage.Temporary;
            var BCs = B.BindingSecurities.OrderBy(x => x.Created.Ticks);
            if (BCs.Count() > 4)
                DB.BindingSecurities.RemoveRange(BCs.Skip(4));

            B.BindingConnections.Add(new Database.BindingConnection
            {
                Id = Guid.NewGuid(),
                MachineName = System.Environment.MachineName,
                ProjectSoftware = (int)ProjectSoftware,
                SignalrConnectionId = Context.ConnectionId,
                IsDeveloper =
#if DEBUG
        true
#else
            false
#endif
            });
            DB.SaveChanges();
            return new OSIC.Shared.Response.binding.Data { Language = (OSIC.Shared.Project.Language)B.UnitTwoLetterIsolanguageName, Region = (OSIC.Shared.Project.Region)B.UnitTwoLetterIsoregionName, Key = ($"{B.Id.ToString()}:{S.Id.ToString()}").Encrypt(), Storage = SCS };
        }
        catch (Exception)
        {
            return await this.Create(ProjectSoftware, ProjectType, UnitType, UnitTwoLetterISORegionName, UnitTwoLetterISOLanguageName, UnitTimeZone);
        }
    }
    public override Task OnDisconnectedAsync(Exception exception)
    {
        using var DB = DBContextFactory.CreateDbContext();
        DB.BindingConnections.RemoveRange(DB.BindingConnections.Where(x => x.SignalrConnectionId == Context.ConnectionId && x.MachineName == System.Environment.MachineName));
        try
        {
            DB.SaveChanges();
        }
        catch (Exception)
        {
        }
        return base.OnDisconnectedAsync(exception);
    }
    public Task SetLanguage(OSIC.Shared.Project.Language Language) {

        Connections.Where(x => x.ConnectionID != Context.ConnectionId && x.MachineName != System.Environment.MachineName).ToList().ForEach(async x => await this.Exchange_Send(((Software)x.Software,x.MachineName), x.ConnectionID, "SetLanguage", Language));
        return Task.CompletedTask;
    }
    public Task SetRegion(Shared.Project.Region Region)
    {
        Connections.Where(x => x.ConnectionID != Context.ConnectionId && x.MachineName != System.Environment.MachineName).ToList().ForEach(async x => await this.Exchange_Send(((Software)x.Software, x.MachineName), x.ConnectionID, "SetRegion", Region));
        return Task.CompletedTask;
    }
    #endregion Handler Binding
}