using System;
namespace Preuss.CredentialService.Cryptography.Abstracts;

public interface IMd5Factory
{
    string ComputeHash(string text);
    byte[] EncodeText(string text);
    string ConvertHashToString(byte[] hash);
}

