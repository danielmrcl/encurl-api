public class InvalidFormException : Exception
{
	public InvalidFormException(string description)
		: base(String.Format("Invalid Form: {0}", description))
	{
	}
}
