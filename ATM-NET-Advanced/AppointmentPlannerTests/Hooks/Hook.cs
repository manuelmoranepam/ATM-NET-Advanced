using AppointmentPlannerTests.Structs;
using ConfigurationLibrary.Interfaces.Configurations;
using FrameworkFacade.FrameworkStartup;
using LoggerLibrary.Interfaces.Factories;
using LoggerLibrary.Interfaces.Loggers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using TechTalk.SpecFlow;
using WebDriverLibrary.Interfaces.WebDrivers;

namespace AppointmentPlannerTests.Hooks;

[Binding]
public sealed class Hook
{
	[BeforeTestRun]
	public static void BeforeTestRun()
	{
		if (Directory.Exists(ConfigurationKey.LogFilePath))
		{
			Directory.Delete(ConfigurationKey.LogFilePath, true);
		}
	}

	[BeforeScenario]
	public void BeforeScenario(ScenarioContext scenarioContext)
	{
		var scenarioId = Guid.NewGuid().ToString();
		var fullFilePath = $"{ConfigurationKey.LogFilePath}\\{scenarioId}\\Log.txt";

		var serviceProvider = new FrameworkService(Directory.GetCurrentDirectory(), ConfigurationKey.ConfigurationFileName)
			.GetServiceProvider().CreateScope().ServiceProvider;

		var configurationService = serviceProvider.GetRequiredService<IConfigurationService>();
		var loggerService = serviceProvider.GetRequiredService<ILoggerServiceFactory>().CreateLoggerService(fullFilePath);
		var webDriverService = serviceProvider.GetRequiredService<IWebDriverService>();

		var url = configurationService.GetConfigurationValue<string>(ConfigurationKey.ApplicationUrl);

		webDriverService.NavigateTo(url);

		scenarioContext[ScenarioContextKey.ServiceProvider] = serviceProvider;
		scenarioContext[ScenarioContextKey.ConfigurationService] = configurationService;
		scenarioContext[ScenarioContextKey.LoggerService] = loggerService;
		scenarioContext[ScenarioContextKey.WebDriverService] = webDriverService;
	}

	[BeforeStep]
	public void BeforeStep(ScenarioContext scenarioContext)
	{
		var logger = scenarioContext[ScenarioContextKey.LoggerService] as ILoggerService;

		var definitionType = scenarioContext.StepContext.StepInfo.StepDefinitionType;
		var stepText = scenarioContext.StepContext.StepInfo.Text;

		logger!.LogInformation($"{definitionType} {stepText}");
	}

	[AfterStep]
	public void AfterStep(ScenarioContext scenarioContext)
	{
		var logger = scenarioContext[ScenarioContextKey.LoggerService] as ILoggerService;

		var status = scenarioContext.StepContext.Status;
		var definitionType = scenarioContext.StepContext.StepInfo.StepDefinitionType;
		var stepText = scenarioContext.StepContext.StepInfo.Text;
		var message = $"{definitionType} {stepText}\n";
		var exception = scenarioContext.TestError;

		LogOutcome(logger!, message, status, exception);
	}

	private static void LogOutcome(ILoggerService logger, string message, ScenarioExecutionStatus status, Exception? exception)
	{
		ArgumentNullException.ThrowIfNull(logger);
		ArgumentException.ThrowIfNullOrWhiteSpace(message);

		var logMessage = $"\t{status} - {message}";

		switch (status)
		{
			case ScenarioExecutionStatus.OK:
				logger.LogInformation(logMessage);
				break;
			case ScenarioExecutionStatus.TestError:
				logger.LogError(exception!, logMessage);
				break;
			case ScenarioExecutionStatus.StepDefinitionPending:
				logger.LogWarning(logMessage);
				break;
			case ScenarioExecutionStatus.UndefinedStep:
				logger.LogWarning(logMessage);
				break;
			case ScenarioExecutionStatus.BindingError:
				logger.LogError(exception!, logMessage);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(status), status, null);
		}
	}

	[AfterScenario]
	public void AfterScenario(ScenarioContext scenarioContext)
	{
		var loggerService = scenarioContext[ScenarioContextKey.LoggerService] as ILoggerService;

		loggerService!.CloseAndFlush();

		var webDriverService = scenarioContext[ScenarioContextKey.WebDriverService] as IWebDriverService;

		webDriverService!.DisposeWebDriver();

		var serviceProvider = scenarioContext[ScenarioContextKey.ServiceProvider] as ServiceProvider;

		serviceProvider!.Dispose();
	}
}
