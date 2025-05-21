namespace api.services;

using api.database;
using api.models;
using api.utils;

public class LinkService
{
	private readonly string _baseUrl;
	private readonly LinkDAO _repository;

	public LinkService(LinkDAO repository)
	{
		this._baseUrl = Environment.GetEnvironmentVariable("ACCESS_LINK_BASE_URL")!;
		this._repository = repository;
	}

	public String FindLink(string code)
	{
		return _repository.FindByCode(code).Url;
	}

	public CreateLinkResponseDTO CreateLink(CreateLinkDTO dto)
	{
		_ValidateDto(dto);
		var link = new Link()
		{
			Code = $"{StringUtil.ExtractHostname(dto.url)}-{StringUtil.GenerateNewLinkCode()}",
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
