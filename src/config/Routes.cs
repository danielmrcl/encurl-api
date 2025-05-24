namespace api.config;

using Microsoft.AspNetCore.Mvc;
using api.services;
using api.models;
using api.utils;

public static class Routes
{
	public static void Map(WebApplication app)
	{
		app.MapGet("/{code}", (string code, LinkService service, HttpContext context) =>
		{
			try
			{
				var metadata = new RequestMetadata(
					IpAddress: context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? "0.0.0.0",
					UserAgent: context.Request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown"
				);
				return Results.Redirect(service.FindLink(code, metadata), true, false);
			}
			catch (Exception e) when (e is LinkException || e is InvalidOperationException)
			{
				return Results.BadRequest(new ErrorDTO(400, e.Message));
			}
		});

		app.MapPost("/api/links", (CreateLinkDTO dto, LinkService service, AuthService authService,
					[FromHeader(Name = "Authorization")] string tokenBasic) =>
		{
			try
			{
				// TODO: Auth middleware to run on all critical or db-persistent endpoints.
				if (!authService.IsAuthorized(tokenBasic))
				{
					return Results.Unauthorized();
				}

				return Results.Ok(service.CreateLink(dto));
			}
			catch (InvalidFormException e)
			{
				return Results.BadRequest(new ErrorDTO(400, e.Message));
			}
		});
	}
}
