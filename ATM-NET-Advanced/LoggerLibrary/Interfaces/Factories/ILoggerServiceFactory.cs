using LoggerLibrary.Interfaces.Loggers;

namespace LoggerLibrary.Interfaces.Factories;

public interface ILoggerServiceFactory
{
	ILoggerService CreateLoggerService(string filePath);
}
