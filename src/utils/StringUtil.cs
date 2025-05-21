namespace api.utils;

using System;
using System.Text;
using System.Text.RegularExpressions;

public class StringUtil {

	private static readonly char[] _availableChars = new char[]
	{
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
		'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
		'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
		'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
	};

	private static readonly string _urlRegExp =	"(https?:\\/\\/|mailto:)(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]"
		+ "{2,256}\\.[a-z]{2,4}\\b([-a-zA-Z0-9@:%_\\+.~#?&//=]*)";
	private static readonly string _aliasRegExp = "^[a-zA-Z0-9_]{4,10}$";

	public static string GenerateNewLinkCode()
	{
		var length = 4;
		var sb = new StringBuilder("", length);
		var rand = new Random();

		for (int i = 0; i < length; i++)
		{
			sb.Append(_availableChars[rand.Next(62)]);
		}

		return sb.ToString();
	}

	public static bool IsValidUrl(string url)
	{
		return Regex.IsMatch(url, _urlRegExp);
	}

	public static bool IsValidAlias(string alias)
	{
		return Regex.IsMatch(alias, _aliasRegExp);
	}
}
