using OSIC.Server.Hosting;
using OSIC.Server.Hosting.server;

string a = Guid.NewGuid() + ":" + Guid.NewGuid();
var b = a.Encrypt();
Console.WriteLine(b);
Console.WriteLine(b.Decrypt());