using System;
using Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;

namespace Preuss.CredentialService.Validation.Abstracts;

public interface ICredentialsValidator
{
    bool IsValidEmail(string email);
    bool IsValidUsername(string username);
    bool IsValidPassword(string password);
    bool IsValidObject(Credentials credentials);
}

