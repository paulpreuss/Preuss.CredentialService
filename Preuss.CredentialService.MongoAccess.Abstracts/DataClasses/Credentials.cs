using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;

public class Credentials
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }
	public string Username { get; set; }
	public string HashedPassword { get; set; }
	public string Email { get; set; }
    public IEnumerable<string> FriendIds { get; set; }
	public IEnumerable<string> FitnessGroupIds { get; set; }
}
