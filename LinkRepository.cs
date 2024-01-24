using MongoDB.Driver;

public class LinkRepository
{
	private readonly IMongoCollection<Link> _client;

	public LinkRepository(DBClient client)
	{
		this._client = client.GetCollection<Link>();
	}

	public void Save(Link link)
	{
		_client.InsertOne(link);
	}
}
