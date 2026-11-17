namespace Encurl.Api.Config;

using Encurl.Api.Services;
using Encurl.Api.Models;
using Encurl.Api.Utils;

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
				var result = Results.Redirect(service.FindLink(code, metadata), true, false);
				Metrics.IncLinkFind();
				return result;
			}
			catch (Exception e) when (e is LinkException || e is InvalidOperationException)
			{
				Metrics.IncLinkFindError();
				return Results.BadRequest(new ErrorDTO(400, e.Message));
			}
		});

		app.MapPost("/api/links", (CreateLinkDTO dto, LinkService service, AuthService authService) =>
		{
			try
			{
				var result = Results.Ok(service.CreateLink(dto));
				Metrics.IncLinkSave();
				return result;
			}
			catch (InvalidFormException e)
			{
				Metrics.IncLinkSaveError();
				return Results.BadRequest(new ErrorDTO(400, e.Message));
			}
		});
	}
}
