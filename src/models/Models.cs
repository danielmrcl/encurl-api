namespace Encurl.Api.Models;

public record CreateLinkDTO(string Url, string? Alias);

public record CreateLinkResponseDTO(string GeneratedUrl);

public record ErrorDTO(ushort Code, string Message);

public record RequestMetadata(string IpAddress, string UserAgent);
