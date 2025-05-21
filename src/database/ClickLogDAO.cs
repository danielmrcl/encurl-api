namespace api.database;

using MongoDB.Driver;
using api.models;

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

