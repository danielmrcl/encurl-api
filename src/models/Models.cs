namespace api.models;

public record CreateLinkDTO(string url, string? alias);

public record CreateLinkResponseDTO(string generatedUrl);

public record ErrorDTO(ushort code, string message);

public record RequestMetadata(string IpAddress, string UserAgent);
