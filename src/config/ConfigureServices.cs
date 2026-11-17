namespace Encurl.Api.Config;

using Encurl.Api.Models;
using Encurl.Api.Services;
using Encurl.Api.Database;
using System.Diagnostics.Metrics;

public static class ConfigureBuilder
{
	public static void Cors(WebApplicationBuilder builder, string specificOriginsPolicyName)
	{
		builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: specificOriginsPolicyName, policy =>
					{
						policy.WithOrigins("http://localhost:5173")
							.WithMethods("POST")
							.AllowAnyHeader();
					});
			});
	}

	public static void Settings(WebApplicationBuilder builder)
	{
		builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));
		builder.Services.Configure<StaticCredentialsSettings>(builder.Configuration.GetSection("StaticCredentials"));
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

	public static void Monitoring(WebApplicationBuilder builder)
	{
		builder.Services.AddLogging();
		builder.Services.AddResourceMonitoring();

		var listener = new MeterListener
		{
			InstrumentPublished = (instrument, meterListener) =>
			{
				if (instrument.Meter.Name == "Microsoft.Extensions.Diagnostics.ResourceMonitoring")
				{
					meterListener.EnableMeasurementEvents(instrument);
				}
			}
		};

		listener.SetMeasurementEventCallback<double>((instrument, measurement, tags, state) =>
			{
				Console.WriteLine($"{instrument.Name}: {measurement}");
			});

		listener.Start();
	}

	public static void Caching(WebApplicationBuilder builder)
	{
		builder.Services.AddMemoryCache();
	}
}
