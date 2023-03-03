using Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;

namespace Preuss.CredentialService.MongoAccess.Abstracts.Repositories;

public interface ICredentialRepository
{
    Task<List<Credentials>> GetAllCredentialsAsync();
    Task<Credentials> GetCredentialsByNameAsync(string name);
    Task AddCredentials(Credentials credentials);
    Task UpdateCredentials(Credentials credentials);
    Task DeteleCredentials(Credentials credentials);
}
