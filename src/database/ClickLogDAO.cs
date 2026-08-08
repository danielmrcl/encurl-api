namespace Encurl.Api.Database;

using Encurl.Api.Models;

public class ClickLogDAO
{
	private readonly DBClient _client;
	private readonly ILogger<ClickLogDAO> _logger;

	public ClickLogDAO(DBClient client, ILogger<ClickLogDAO> logger)
	{
		this._client = client;
		this._logger = logger;
	}

	public void Save(ClickLog clickLog)
	{
		String sqlQuery = @"INSERT INTO ClickLog (Id, LinkId, Metadata, CreatedAt)
		VALUES (@Id, @LinkId, @Metadata, @CreatedAt)";
		var parameters = new Dictionary<string, object>();

		parameters.Add("Id", clickLog.Id.ToString());
		parameters.Add("LinkId", clickLog.LinkId.ToString());
		parameters.Add("Metadata", clickLog.Metadata);
		parameters.Add("CreatedAt", DateTime.Now);

		_client.RunUpdateQuery(sqlQuery, parameters);
		_logger.LogInformation($"ClickLog {clickLog.Id} saved successfuly");
	}
}

