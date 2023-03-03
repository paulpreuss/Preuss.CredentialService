using System;
using System.Security.Cryptography;
using System.Text;
using Preuss.CredentialService.Cryptography.Abstracts;

namespace Preuss.CredentialService.Cryptography;

public class Md5Factory : IMd5Factory
{
	public string ComputeHash(string text)
	{
        var encodedText = EncodeText(text);
        var hashedText = MD5.HashData(encodedText);

        return ConvertHashToString(hashedText);
    }

    public byte[] EncodeText(string text)
    {
        return Encoding.Default.GetBytes(text);
    }

    public string ConvertHashToString(byte[] hash)
    {
        return BitConverter.ToString(hash);
    }
}