using Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;

namespace Preuss.CredentialService.Abstracts.Processors;

public interface ICredentialProcessor
{
    Task<Credentials> LoginAsync(string username, string password);
    Task CreateUserAsync(Credentials credentials, string password);
    Task UpdateUserAsync(Credentials credentials);
    Task UpdateUserPasswordAsync(Credentials credentials, string password);
}
