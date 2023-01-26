using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OSIC.Shared.Project;
public static class Helper
{
    public static string Domain(this Software Software) => Software switch
    {
        Software.InterConnecting => "interconnecting.info",
        Software.OBJECTSOCIAL => "object.social",
        Software.Claims => "run.claims",
        Software.BadClaims => "bad.claims",
        Software.BetweenClaims => "between.claims",
        Software.GoodClaims => "good.claims",
        Software.MemoryClaims => "memory.claims",
        Software.MemoriesClaims => "memories.claims",
        _ => throw new NotImplementedException(),
    };
}