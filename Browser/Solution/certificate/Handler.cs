using OSIC.Shared.Certificate;
using System.Net.Http.Json;

namespace OSIC.Browser.Solution.certificate;
class Handler : OSIC.Shared.Layout.certificate.Handler
{
    private readonly HttpClient HttpClient;
    public async Task<string> Get()=> await HttpClient.GetStringAsync("/OSIC.Hosting.certificate.Get");
    public async Task<bool> Have()=> await HttpClient.GetFromJsonAsync<bool>("/OSIC.Hosting.certificate.Have");
    public async void Set(string Key, Storage Storage) => await HttpClient.PostAsJsonAsync("/OSIC.Hosting.certificate.Save", new Shared.Certificate.Browser(Key, Storage));
    public Handler(HttpClient HttpClient)
    {
        this.HttpClient = HttpClient;
    }
}
