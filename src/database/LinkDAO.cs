namespace Encurl.Api.Database;

using Encurl.Api.Models;

public class LinkDAO
{
	private readonly DBClient _client;
	private readonly ILogger<LinkDAO> _logger;

	public LinkDAO(DBClient client, ILogger<LinkDAO> logger)
	{
		this._client = client;
		this._logger = logger;
	}

	public void Save(Link link)
	{
		String sqlQuery = @"INSERT INTO Links (Id, Code, Url, CreatedAt)
		VALUES (@Id, @Code, @Url, @CreatedAt)";
		var parameters = new Dictionary<string, object>();

		parameters.Add("Id", link.Id.ToString());
		parameters.Add("Code", link.Code);
		parameters.Add("Url", link.Url);
		parameters.Add("CreatedAt", DateTime.Now);

		_client.RunUpdateQuery(sqlQuery, parameters);
		_logger.LogInformation($"Link {link.Id} saved successfuly");
	}

	public Link FindByCode(string code)
	{
		String sqlQuery = @"SELECT l.* FROM Links l
			WHERE l.Code = @Code";
		var parameters = new Dictionary<string, object>();

		parameters.Add("Code", code);

		var links = _client.RunSelectQuery(sqlQuery, parameters);
		_logger.LogInformation($"Finded {links.Count} links by code {code}");
		return links.First();
	}

	public bool ExistsByCode(string code)
	{
		String sqlQuery = @"SELECT l.* FROM Links l
			WHERE l.Code = @Code";
		var parameters = new Dictionary<string, object>();

		parameters.Add("Code", code);

		var links = _client.RunSelectQuery(sqlQuery, parameters);
		_logger.LogInformation($"Finded {links.Count} links by code {code}");
		return links.Count > 0;
	}
}
