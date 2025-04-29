public record CreateLinkDTO(string url);

public record CreateLinkResponseDTO(string generatedUrl);

public record ErrorDTO(ushort code, string message);

public class DatabaseSettings
{
	public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
	public string LinksCollectionName { get; set; } = null!;
}

public class StaticCretentialsSettings
{
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
}
