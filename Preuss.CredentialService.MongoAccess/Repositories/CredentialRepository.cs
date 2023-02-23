using System;
using Preuss.CredentialService.MongoAccess.Abstracts.Repositories;

namespace Preuss.CredentialService.MongoAccess.Repositories;

public class CredentialRepository : ICredentialRepository
{
    private readonly string _connectionString;

    public CredentialRepository(string connectionString)
	{
		_connectionString = connectionString;
	}
}
