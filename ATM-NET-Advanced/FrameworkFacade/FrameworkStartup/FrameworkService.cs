using ConfigurationLibrary.Configurations;
using ConfigurationLibrary.Interfaces.Configurations;
using Microsoft.Extensions.DependencyInjection;
using System;

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
			.BuildServiceProvider();
	}

	public IServiceProvider GetServiceProvider()
	{
		return _serviceProvider;
	}
}
