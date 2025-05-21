namespace api.config;

using Microsoft.AspNetCore.Mvc;
using api.services;
using api.models;
using api.utils;

public static class Routes
{
	public static void Map(WebApplication app)
	{
		app.MapGet("/{code}", (string code, LinkService service) =>
		{
			try
			{
				return Results.Redirect(service.FindLink(code), true, false);
			}
			catch (InvalidOperationException e)
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
