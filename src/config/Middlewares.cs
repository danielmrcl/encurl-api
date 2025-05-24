namespace api.config;

using api.services;

public static class Middlewares
{
	public static void Configure(WebApplication app)
	{
		app.Use(async (context, next) =>
		{
			if (context.Request.Method == "POST" || context.Request.Method == "PUT")
			{
				var authService = context.RequestServices.GetRequiredService<AuthService>();

				var tokenBasic = context.Request.Headers["Authorization"].FirstOrDefault() ?? "";

				if (!authService.IsAuthorized(tokenBasic))
				{
				context.Response.StatusCode = 401;
				return;
				}
			}
			
			await next();
		});
	}
}
