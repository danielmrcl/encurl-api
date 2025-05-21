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
		_ValidateLink(dto);

		var link = new Link() { Code = _GetCode(dto), Url = dto.url };
		_repository.Save(link);
		return new CreateLinkResponseDTO($"{_baseUrl}/{link.Code}");
	}

	private void _ValidateLink(CreateLinkDTO dto)
	{
		if (!StringUtil.IsValidUrl(dto.url))
		{
			throw new InvalidFormException("url is invalid");
		}

		if (!string.IsNullOrWhiteSpace(dto.alias))
		{
			if (!StringUtil.IsValidAlias(dto.alias))
			{
				throw new InvalidFormException("alias is invalid");
			}
			if (_repository.ExistsByCode(dto.alias))
			{
				throw new InvalidFormException("alias is already in use");
			}
		}
	}

	private string _GetCode(CreateLinkDTO dto)
	{
		if (string.IsNullOrWhiteSpace(dto.alias))
		{
			return StringUtil.GenerateNewLinkCode();
		}
		return dto.alias;
	}

}
