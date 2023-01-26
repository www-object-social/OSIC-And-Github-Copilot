using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Server.Hosting.server;

public static class Setting
{
    public static string Path => System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
    private static string CombinePathAndFile => System.IO.Path.Combine(Path, "Server.OSIC");
    private static bool PathAndFileExist => System.IO.File.Exists(CombinePathAndFile);
    private static string SafetyWord => PathAndFileExist ? System.IO.File.ReadAllText(CombinePathAndFile) : throw new Exception(string.Join(System.Environment.NewLine, new[] { $"OSIC.Server.Hosting.database.Connection", $"Missing file:{CombinePathAndFile}", "Must contain safety GUID." }));
#pragma warning disable SYSLIB0041
    public static string Encrypt(this byte[] Bytes) {
        using var aes = Aes.Create();
        using var rfc = new Rfc2898DeriveBytes(SafetyWord, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        aes.Key = rfc.GetBytes(32);
        aes.IV = rfc.GetBytes(16);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(Bytes, 0, Bytes.Length);
        cs.Close();
        return Convert.ToBase64String(ms.ToArray());
    }
    public static string Encrypt(this string clearText) => Encoding.Unicode.GetBytes(clearText).Encrypt();
    public static string Decrypt(this byte[] Bytes) { 
        using var aes = Aes.Create();
        using var rfc = new Rfc2898DeriveBytes(SafetyWord, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        aes.Key = rfc.GetBytes(32);
        aes.IV = rfc.GetBytes(16);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(Bytes, 0, Bytes.Length);
        cs.Close();
        return Encoding.Unicode.GetString(ms.ToArray());
    }
    public static string Decrypt(this string cipherText) => Convert.FromBase64String(cipherText.Replace(" ", "+")).Decrypt();
#pragma warning restore SYSLIB004
}
