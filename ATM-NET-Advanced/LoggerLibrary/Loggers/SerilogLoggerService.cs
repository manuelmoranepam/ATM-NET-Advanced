using ConfigurationLibrary.Interfaces.Configurations;
using LoggerLibrary.Interfaces.Loggers;
using Serilog;
using Serilog.Core;
using System;

namespace LoggerLibrary.Loggers;

public class SerilogLoggerService : ILoggerService
{
	private readonly Logger _logger;

	public SerilogLoggerService(IConfigurationService configurationService)
	{
		ArgumentNullException.ThrowIfNull(configurationService);

		var loggerConfiguration = new LoggerConfiguration()
			.ReadFrom.Configuration(configurationService.GetConfiguration())
			.Enrich.FromLogContext();

		_logger = loggerConfiguration.CreateLogger();
	}

	public void LogDebug(string message, params object?[]? args) => _logger.Debug(message, args);
	public void LogInformation(string message, params object?[]? args) => _logger.Information(message, args);
	public void LogWarning(string message, params object?[]? args) => _logger.Warning(message, args);
	public void LogError(Exception exception, string message, params object?[]? args) => _logger.Error(exception, message, args);
	public void LogCritical(Exception exception, string message, params object?[]? args) => _logger.Fatal(exception, message, args);
	public void CloseAndFlush() => _logger.Dispose();
}
