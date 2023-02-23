using System;
namespace Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;

public class Credentials
{
	public string Id { get; set; }
	public string Username { get; set; }
	public string HashedPassword { get; set; }
	public string Email { get; set; }
}
