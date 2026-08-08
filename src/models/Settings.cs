namespace Encurl.Api.Models;

public class DatabaseSettings
{
	public string ConnectionString { get; set; } = null!;
}

public class StaticCretentialsSettings
{
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
}
