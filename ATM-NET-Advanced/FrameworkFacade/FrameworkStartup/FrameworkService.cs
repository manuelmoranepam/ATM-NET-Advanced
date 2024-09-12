using ConfigurationLibrary.Configurations;
using ConfigurationLibrary.Interfaces.Configurations;
using LoggerLibrary.Interfaces.Loggers;
using LoggerLibrary.Loggers;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebDriverLibrary.Configurations;
using WebDriverLibrary.Interfaces.Configurations;
using WebDriverLibrary.Interfaces.WebDrivers;
using WebDriverLibrary.WebDrivers;

namespace FrameworkFacade.FrameworkStartup;

public class FrameworkService
{
	private readonly IServiceProvider _serviceProvider;

	public FrameworkService(string filePath, string fileName)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
		ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

		_serviceProvider = new ServiceCollection()
			.AddSingleton<IConfigurationService>(new ConfigurationService(filePath, fileName))
			.AddScoped<ILoggerService, SerilogLoggerService>()
			.AddSingleton<IWebDriverConfiguration, WebDriverConfiguration>()
			.AddScoped<IWebDriverService, SeleniumWebDriverService>()
			.BuildServiceProvider();
	}

	public IServiceProvider GetServiceProvider()
	{
		return _serviceProvider;
	}
}
