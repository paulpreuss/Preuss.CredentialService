using MongoDB.Driver;
using Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;
using Preuss.CredentialService.MongoAccess.Abstracts.Repositories;

namespace Preuss.CredentialService.MongoAccess.Repositories;

public class CredentialRepository : ICredentialRepository
{
    private readonly IMongoCollection<Credentials> _collection;

    public CredentialRepository(string connectionString)
	{
		var databaseName = "credential_service";
		var collectionName = "credentials";

        var client = new MongoClient(connectionString);
		var db = client.GetDatabase(databaseName);

		_collection = db.GetCollection<Credentials>(collectionName);
	}

	public async Task<List<Credentials>> GetAllCredentialsAsync()
	{
		var result = await _collection.FindAsync(_ => true);

		return result.ToList();
	}

	public async Task<Credentials> GetCredentialsByNameAsync(string name)
	{
		var result = await _collection.FindAsync(x => x.Username == name);

		return (Credentials)result;
	}

	public Task AddCredentials(Credentials credentials)
	{
		return _collection.InsertOneAsync(credentials);
	}

	public Task UpdateCredentials(Credentials credentials)
	{
		var filter = Builders<Credentials>.Filter.Eq("Id", credentials.Id);

		return _collection.ReplaceOneAsync(filter, credentials,
			new ReplaceOptions { IsUpsert = true });
	}

	public Task DeteleCredentials(Credentials credentials)
	{
		return _collection.DeleteOneAsync(x => x.Id == credentials.Id);
	}
}
