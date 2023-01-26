using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Server.Hosting.certificate;
[ApiController]
public class Controller: ControllerBase
{
    [HttpGet("/OSIC.Hosting.certificate.Have")]
    public bool Have() => HttpContext.Session.Keys.Any(x => x == "OSIC.Server.Hosting.certificate.Session") || Request.Cookies.Any(x => x.Key == "OSIC.Server.Hosting.certificate.Cookie");
    [HttpPost("/OSIC.Hosting.certificate.Save")]
    public void Save(OSIC.Shared.Certificate.Browser Browser)
    {
        if (Browser.Storage == OSIC.Shared.Certificate.Storage.Local)
        {
            Response.Cookies.Append("OSIC.Server.Hosting.certificate.Cookie", Browser.Key, new CookieOptions { Expires = DateTime.UtcNow.AddDays(60), HttpOnly = true, IsEssential = true, SameSite = SameSiteMode.Lax, Secure = true });
            if (HttpContext.Session.Keys.Any(x => x == "OSIC.Server.Hosting.certificate.Session"))
                HttpContext.Session.Remove("OSIC.Server.Hosting.certificate.Session");
            return;
        }
        if (Request.Cookies.Any(x => x.Key == "OSIC.Server.Hosting.certificate.Cookie"))
            Response.Cookies.Delete("OSIC.Server.Hosting.certificate.Cookie");
        HttpContext.Session.SetString("OSIC.Server.Hosting.certificate.Session", Browser.Key);
    }
    [HttpGet("/OSIC.Hosting.certificate.Get")]
    public string Get() => Request.Cookies.Any(x => x.Key == "OSIC.Server.Hosting.certificate.Cookie") ? Request.Cookies["OSIC.Server.Hosting.certificate.Cookie"] : HttpContext.Session.GetString("OSIC.Server.Hosting.certificate.Session");
}
