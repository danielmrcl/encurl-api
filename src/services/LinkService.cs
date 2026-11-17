namespace Encurl.Api.Services;

using Encurl.Api.Database;
using Encurl.Api.Models;
using Encurl.Api.Utils;
using Microsoft.Extensions.Caching.Memory;

public class LinkService
{
	private readonly string _baseUrl;
	private readonly LinkDAO _repository;
	private readonly ClickLogService _clickLogService;
	private readonly ILogger<LinkService> _logger;
	private readonly IMemoryCache _cache;

	public LinkService(LinkDAO repository, ClickLogService clickLogService, ILogger<LinkService> logger,
			IMemoryCache cache)
	{
		this._baseUrl = Environment.GetEnvironmentVariable("ACCESS_LINK_BASE_URL")!;
		this._repository = repository;
		this._clickLogService = clickLogService;
		this._logger = logger;
		this._cache = cache;
	}

	public String FindLink(string code, RequestMetadata metadata)
	{
		var cacheKey = $"link_{code}";
		var cachedLink = _cache.Get<CachedLink>(cacheKey);

		if (cachedLink != null)
		{
			if (cachedLink.isError || cachedLink.Url == null)
			{
				throw new LinkException("code not found");
			}
			return cachedLink.Url;
		}

		var link = _repository.FindByCode(code);

		if (link == null) {
			_cache.Set<CachedLink>(cacheKey, new CachedLink(true, null), TimeSpan.FromDays(1));
			throw new LinkException("code not found");
		}

		_cache.Set<CachedLink>(cacheKey, new CachedLink(false, link.Url), TimeSpan.FromDays(15));
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
