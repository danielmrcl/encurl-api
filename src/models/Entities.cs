namespace Encurl.Api.Models;

public class Link
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public required string Code { get; set; }
	public required string Url { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class ClickLog
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public required Guid LinkId { get; set; }
	public required string Metadata { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
}
