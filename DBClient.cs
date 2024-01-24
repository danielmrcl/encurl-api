using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Reflection;

public class DBClient
{
	private readonly IMongoDatabase _db;
	private readonly DatabaseSettings _dbOptions;

	public DBClient(IOptions<DatabaseSettings> dbOptions)
	{
		var dbClient = new MongoClient(dbOptions.Value.ConnectionString);
		_db = dbClient.GetDatabase(dbOptions.Value.DatabaseName);
		_dbOptions = dbOptions.Value;
	}

	public IMongoCollection<T> GetCollection<T>()
	{
		return _db.GetCollection<T>(_getCollectionName(typeof(T).Name));
	}

	private string _getCollectionName(string className)
	{
		return className switch {
			"Link" => _dbOptions.LinksCollectionName,
			_ => throw new ArgumentOutOfRangeException(className, "Collection Name Not Registered")
		};
	}
}
