using System;
using System.IO;
using System.Text.RegularExpressions;
using Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;
using Preuss.CredentialService.Validation.Abstracts;

namespace Preuss.CredentialService.Validation;

public class CredentialsValidator : ICredentialsValidator
{
	public bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        var match = emailRegex.Match(email);

        return match.Success;
    }

    public bool IsValidObject(Credentials credentials)
    {
        return IsValidEmail(credentials.Email)
            && IsValidPassword(credentials.HashedPassword)
            && IsValidUsername(credentials.Username);
    }

    public bool IsValidPassword(string password)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");

        return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password)
            && hasMinimum8Chars.IsMatch(password);
    }

    public bool IsValidUsername(string username)
    {
        var usernameRegex = new Regex("^[a-zA-Z0-9]+$");
        var match = usernameRegex.Match(username);

        return match.Success;
    }
}

