namespace api.database;

using MongoDB.Driver;
using api.models;

public class LinkDAO
{
	private readonly IMongoCollection<Link> _client;

	public LinkDAO(DBClient client)
	{
		this._client = client.GetCollection<Link>();
	}

	public void Save(Link link)
	{
		_client.InsertOne(link);
	}

	public Link FindByCode(string code)
	{
		var filter = new FilterDefinitionBuilder<Link>()
			.Eq(v => v.Code, code);
		return _client.FindSync(filter).Single();
	}
}
