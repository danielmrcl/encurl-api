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

	public Link FindByCode(string code)
	{
		var filter = new FilterDefinitionBuilder<Link>()
			.Eq(v => v.Code, code);
		return _client.FindSync(filter).Single();
	}
}
