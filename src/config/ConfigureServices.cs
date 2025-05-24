namespace Encurl.Api.Config;

using Encurl.Api.Models;
using Encurl.Api.Services;
using Encurl.Api.Database;

public static class ConfigureBuilder
{
	public static void Settings(WebApplicationBuilder builder)
	{
		builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));
		builder.Services.Configure<StaticCretentialsSettings>(builder.Configuration.GetSection("StaticCretentials"));
	}

	public static void Singletons(WebApplicationBuilder builder)
	{
		builder.Services.AddSingleton<LinkService>();
		builder.Services.AddSingleton<AuthService>();
		builder.Services.AddSingleton<ClickLogService>();
		builder.Services.AddSingleton<DBClient>();
		builder.Services.AddSingleton<LinkDAO>();
		builder.Services.AddSingleton<ClickLogDAO>();
	}
}
