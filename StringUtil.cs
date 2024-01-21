using System;
using System.Text;

public class StringUtil {

	private static readonly char[] _availableChars = new char[]
	{
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
		'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
		'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
		'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
	};

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
}
