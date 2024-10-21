using ConfigurationLibrary.Configurations;
using ConfigurationLibrary.Interfaces.Configurations;
using LoggerLibrary.Factories;
using LoggerLibrary.Interfaces.Factories;
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
		_serviceProvider = new ServiceCollection()
			.AddSingleton<IConfigurationService>(new ConfigurationService(filePath, fileName))
			.AddScoped<ILoggerServiceFactory, LoggerServiceFactory>()
			.AddSingleton<IWebDriverConfiguration, WebDriverConfiguration>()
			.AddScoped<IWebDriverService, SeleniumWebDriverService>()
			.BuildServiceProvider();
	}

	public IServiceProvider GetServiceProvider()
	{
		return _serviceProvider;
	}
}
