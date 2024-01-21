using System.Collections.Generic;

public class LinkService
{
	private readonly string _baseUrl = Environment.GetEnvironmentVariable("ACCESS_LINK_BASE_URL")!;

	public Dictionary<string, string> CreateLink(CreateLinkDTO dto)
	{
		var linkCode = StringUtil.GenerateNewLinkCode();
		var result = new Dictionary<string, string>();
		result.Add("generatedUrl", $"{_baseUrl}/{linkCode}");
		return result;
	}

}
