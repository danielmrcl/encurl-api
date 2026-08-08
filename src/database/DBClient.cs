namespace Encurl.Api.Database;

using System.Data.SQLite;
using Microsoft.Extensions.Options;
using Encurl.Api.Models;

public class DBClient
{
	private readonly SQLiteConnection _db;
	private readonly DatabaseSettings _dbOptions;

	public DBClient(IOptions<DatabaseSettings> dbOptions)
	{
		_db = new SQLiteConnection(dbOptions.Value.ConnectionString);
		_dbOptions = dbOptions.Value;
	}

	public void RunUpdateQuery(String sqlQuery, Dictionary<string, object> parameters)
	{
		try
		{
			SQLiteCommand command = new SQLiteCommand(sqlQuery, _db);

			foreach(var entry in parameters)
			{
				command.Parameters.AddWithValue($"@{entry.Key}", entry.Value);
			}

			_db.Open();
			int rows = command.ExecuteNonQuery();
			return;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			_db.Close();
		}
	}

	public List<Link> RunSelectQuery(String sqlQuery, Dictionary<string, object> parameters)
	{
		try
		{
			SQLiteCommand command = new SQLiteCommand(sqlQuery, _db);

			foreach(var entry in parameters)
			{
				command.Parameters.AddWithValue($"@{entry.Key}", entry.Value);
			}

			_db.Open();
			var reader = command.ExecuteReader();

			var links = new List<Link> {};
			while (reader.Read())
			{
				var link = new Link() {
					Id = Guid.Parse(reader.GetString(0)),
					Code = reader.GetString(1),
					Url = reader.GetString(2),
					CreatedAt = reader.GetDateTime(3)
				};
				links.Add(link);
			}

			return links;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			_db.Close();
		}
	}
}
