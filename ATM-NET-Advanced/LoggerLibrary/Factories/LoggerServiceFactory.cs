using ConfigurationLibrary.Interfaces.Configurations;
using LoggerLibrary.Interfaces.Factories;
using LoggerLibrary.Interfaces.Loggers;
using LoggerLibrary.Loggers;

namespace LoggerLibrary.Factories;

public class LoggerServiceFactory : ILoggerServiceFactory
{
	private readonly IConfigurationService _configurationService;

	public LoggerServiceFactory(IConfigurationService configurationService)
	{
		_configurationService = configurationService;
	}

	public ILoggerService CreateLoggerService(string filePath)
	{
		return new SerilogLoggerService(_configurationService, filePath);
	}
}
