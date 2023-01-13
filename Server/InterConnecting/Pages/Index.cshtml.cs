using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace OSIC.Server.InterConnecting.Pages;
public class IndexModel : PageModel
{
    internal IDbContextFactory<Database.Context> Context { get; init; } = null!;
    public IndexModel(IDbContextFactory<Database.Context> Context)
    {
        var a = Context != null;
        var b = a;
    }
    public void OnGet()
    {
    }
}
