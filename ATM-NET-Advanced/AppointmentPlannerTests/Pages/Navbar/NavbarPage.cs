using LoggerLibrary.Interfaces.Loggers;
using OpenQA.Selenium;
using System;
using WebDriverLibrary.Interfaces.WebDrivers;

namespace AppointmentPlannerTests.Pages.Navbar;

public partial class NavbarPage
{
	private readonly IWebDriverService _webDriverService;
	private readonly ILoggerService _loggerService;
	private readonly IWebDriver _webDriver;

	public NavbarPage(IWebDriverService webDriverService, ILoggerService loggerService)
	{
		ArgumentNullException.ThrowIfNull(webDriverService);
		ArgumentNullException.ThrowIfNull(loggerService);

		_webDriverService = webDriverService;
		_loggerService = loggerService;
		_webDriver = _webDriverService.GetWebDriver();

		_loggerService.LogInformation(_pageInitializedMessage);
	}

	public void NavigateToDoctorsPage()
	{
		ClickDoctorsMenuItem();
	}
}
