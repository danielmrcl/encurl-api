namespace Encurl.Api.Models;

public class DatabaseSettings
{
	public string ConnectionString { get; set; } = null!;
}

public class StaticCredentialsSettings
{
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
}
