namespace api.models;

public record CreateLinkDTO(string url);

public record CreateLinkResponseDTO(string generatedUrl);

public record ErrorDTO(ushort code, string message);

