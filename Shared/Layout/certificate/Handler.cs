namespace OSIC.Shared.Layout.certificate;
public interface Handler
{
    public Task<bool> Have();
    public Task<string> Get();
    public void Set(string Key, OSIC.Shared.Certificate.Storage Storage); 
}