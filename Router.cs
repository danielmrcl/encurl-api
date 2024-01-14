using System.Collections.Generic;

public class Router
{
	private string BASE_URL = Environment.GetEnvironmentVariable("ACCESS_LINK_BASE_URL")!;

	public Dictionary<string, string> PostLinks(GenerateLinkDTO dto)
	{
		var linkCode = StringUtil.GenerateNewLinkCode();
		var result = new Dictionary<string, string>();
		result.Add("generatedUrl", $"{BASE_URL}/{linkCode}");
		return result;
	}

}
