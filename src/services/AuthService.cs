namespace Encurl.Api.Services;

using System;
using System.Text;
using Microsoft.Extensions.Options;
using Encurl.Api.Models;

public class AuthService
{
	private readonly string _staticUsername;
	private readonly string _staticPassword;

	private ILogger<AuthService> _logger;

	public AuthService(ILogger<AuthService> logger,
			IOptions<StaticCredentialsSettings> credentialOptions)
	{
		_logger = logger;
		if (string.IsNullOrEmpty(credentialOptions.Value.Username)
				|| string.IsNullOrEmpty(credentialOptions.Value.Password))
		{
			throw new Exception("StaticCredentialsNotFoundException");
		}
		_staticUsername = credentialOptions.Value.Username;
		_staticPassword = credentialOptions.Value.Password;
	}

	public bool IsAuthorized(string token)
	{
		_logger.LogInformation("decoding token");
		if (!token.StartsWith("Basic "))
		{
			_logger.LogError("token is not Basic");
			return false;
		}

		var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token.Substring(6)));
		var username = decoded.Split(":")[0];
		var password = decoded.Split(":")[1];
		_logger.LogInformation("checking username and password");
		return username == _staticUsername && password == _staticPassword;
	}
}
