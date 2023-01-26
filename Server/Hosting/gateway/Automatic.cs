using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Server.Hosting.gateway
{
    class Automatic:OSIC.Server.Hosting.Automatic
    {
        public Automatic(IDbContextFactory<Database.Context> DBContextFactory)
        {
            using var DB = DBContextFactory.CreateDbContext();
            if (DB.Bindings.Any(x => x.Expires < DateTime.UtcNow))
            {
                DB.Bindings.RemoveRange(DB.Bindings.Where(x => x.Expires < DateTime.UtcNow));
                try
                {
                    DB.SaveChanges();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
