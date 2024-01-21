using System.Collections.Generic;

public class LinkService
{
	private readonly string _baseUrl = Environment.GetEnvironmentVariable("ACCESS_LINK_BASE_URL")!;

	public CreateLinkResponseDTO CreateLink(CreateLinkDTO dto)
	{
		_ValidateDto(dto);
		var linkCode = StringUtil.GenerateNewLinkCode();
		return new CreateLinkResponseDTO($"{_baseUrl}/{linkCode}");
	}

	private void _ValidateDto(CreateLinkDTO dto)
	{
		if (!StringUtil.IsValidUrl(dto.url))
		{
			throw new InvalidFormException("url is invalid");
		}
	}

}
