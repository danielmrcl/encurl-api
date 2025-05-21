namespace api.services;

using api.database;
using api.models;

public class ClickLogService
{
	private ClickLogDAO _repository;
	private ILogger<ClickLogService> _logger;

	public ClickLogService(ClickLogDAO repository, ILogger<ClickLogService> logger)
	{
		this._repository = repository;
		this._logger = logger;
	}

	public void Save(Link link, string ipAddress)
	{
		try
		{
			var clickLog = new ClickLog() { LinkId = link.Id, IpAddress = ipAddress };
			_repository.Save(clickLog);
			_logger.LogInformation("[{0}] Click Log Saved", clickLog.LinkId);
		}
		catch (Exception e)
		{
			_logger.LogError(e.Message);
		}
	}
}
