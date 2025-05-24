namespace Encurl.Api.Services;

using Encurl.Api.Database;
using Encurl.Api.Models;
using Encurl.Api.Utils;

public class LinkService
{
	private readonly string _baseUrl;
	private readonly LinkDAO _repository;
	private readonly ClickLogService _clickLogService;
	private readonly ILogger<LinkService> _logger;

	public LinkService(LinkDAO repository, ClickLogService clickLogService, ILogger<LinkService> logger)
	{
		this._baseUrl = Environment.GetEnvironmentVariable("ACCESS_LINK_BASE_URL")!;
		this._repository = repository;
		this._clickLogService = clickLogService;
		this._logger = logger;
	}

	public String FindLink(string code, RequestMetadata metadata)
	{
		var link = _repository.FindByCode(code);

		if (link == null) {
			throw new LinkException("code not found");
		}

		Task.Run(() => _clickLogService.Save(link, metadata));

		return link.Url;
	}

	public CreateLinkResponseDTO CreateLink(CreateLinkDTO dto)
	{
		_ValidateLink(dto);

		var link = new Link() { Code = _GetCode(dto), Url = dto.Url };
		_repository.Save(link);
		return new CreateLinkResponseDTO($"{_baseUrl}/{link.Code}");
	}

	private void _ValidateLink(CreateLinkDTO dto)
	{
		if (!StringUtil.IsValidUrl(dto.Url))
		{
			throw new InvalidFormException("url is invalid");
		}

		if (!string.IsNullOrWhiteSpace(dto.Alias))
		{
			if (!StringUtil.IsValidAlias(dto.Alias))
			{
				throw new InvalidFormException("alias is invalid");
			}
			if (_repository.ExistsByCode(dto.Alias))
			{
				throw new InvalidFormException("alias is already in use");
			}
		}
	}

	private string _GetCode(CreateLinkDTO dto)
	{
		if (string.IsNullOrWhiteSpace(dto.Alias))
		{
			return StringUtil.GenerateNewLinkCode();
		}
		return dto.Alias;
	}

}
