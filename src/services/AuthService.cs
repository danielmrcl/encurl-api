namespace api.services;

using System;
using System.Text;
using Microsoft.Extensions.Options;
using api.models;

public class AuthService
{
	private readonly string _staticUsername;
	private readonly string _staticPassword;

	public AuthService(IOptions<StaticCretentialsSettings> credentialOptions)
	{
		_staticUsername = credentialOptions.Value.Username;
		_staticPassword = credentialOptions.Value.Password;
	}

	public bool IsAuthorized(string token)
	{
		if (!token.StartsWith("Basic "))
		{
			return false;
		}

		var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token.Substring(6)));
		var username = decoded.Split(":")[0];
		var password = decoded.Split(":")[1];
		return username == _staticUsername && password == _staticPassword;
	}
}
