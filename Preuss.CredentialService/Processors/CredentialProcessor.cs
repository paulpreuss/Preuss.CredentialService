using System;
using System.Text;
using System.Security.Cryptography;
using Preuss.CredentialService.Abstracts.Processors;
using Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;
using Preuss.CredentialService.MongoAccess.Abstracts.Repositories;
using Preuss.CredentialService.Cryptography.Abstracts;

namespace Preuss.CredentialService.Processors;

public class CredentialProcessor : ICredentialProcessor
{
	private readonly IMd5Factory _md5Factory;
	private readonly ICredentialRepository _repository;

	public CredentialProcessor(IMd5Factory md5Factory,
		ICredentialRepository repository)
	{
		_md5Factory = md5Factory;
		_repository = repository;
	}

	public async Task<Credentials> LoginAsync(string username, string password)
    {
		var hashedPassword = _md5Factory.ComputeHash(password);

        return await _repository.GetCredentialsByNameAndPasswordAsync(username,
			hashedPassword);
    }

	public async Task CreateUserAsync(Credentials credentials, string password)
	{
		credentials.HashedPassword = _md5Factory.ComputeHash(password);

		await Task.Run(() => _repository.AddCredentials(credentials));
	}

	public async Task UpdateUserAsync(Credentials credentials)
	{
		await Task.Run(() => _repository.UpdateCredentials(credentials));
	}

    public async Task UpdateUserPasswordAsync(Credentials credentials,
		string password)
    {
		credentials.HashedPassword = _md5Factory.ComputeHash(password);

        await Task.Run(() => _repository.UpdateCredentials(credentials));
    }

    public async Task DeleteUserAsync(Credentials credentials)
	{
		await Task.Run(() => _repository.DeteleCredentials(credentials));
	}
}
