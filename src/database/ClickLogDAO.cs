namespace Encurl.Api.Database;

using MongoDB.Driver;
using Encurl.Api.Models;

public class ClickLogDAO
{
	private readonly IMongoCollection<ClickLog> _client;

	public ClickLogDAO(DBClient client)
	{
		this._client = client.GetCollection<ClickLog>();
	}

	public void Save(ClickLog clickLog)
	{
		_client.InsertOne(clickLog);
	}
}

