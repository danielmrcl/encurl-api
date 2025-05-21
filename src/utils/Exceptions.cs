namespace api.utils;

public class InvalidFormException : Exception
{
	public InvalidFormException(string description)
		: base(String.Format("Invalid Form: {0}", description))
	{
	}
}

public class LinkException : Exception
{
	public LinkException(string description)
		: base(String.Format("Link error: {0}", description))
	{
	}
}
