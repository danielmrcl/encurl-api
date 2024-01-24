using System.Collections.Generic;

public class LinkService
{
	private readonly string _baseUrl;
	private readonly LinkRepository _repository;

	public LinkService(LinkRepository repository)
	{
		this._baseUrl = Environment.GetEnvironmentVariable("ACCESS_LINK_BASE_URL")!;
		this._repository = repository;
	}

	public CreateLinkResponseDTO CreateLink(CreateLinkDTO dto)
	{
		_ValidateDto(dto);
		var link = new Link()
		{
			Code = StringUtil.GenerateNewLinkCode(),
			Url = dto.url
		};
		_repository.Save(link);
		return new CreateLinkResponseDTO($"{_baseUrl}/{link.Code}");
	}

	private void _ValidateDto(CreateLinkDTO dto)
	{
		if (!StringUtil.IsValidUrl(dto.url))
		{
			throw new InvalidFormException("url is invalid");
		}
	}

}
