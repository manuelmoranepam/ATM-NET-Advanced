using AppointmentPlannerTests.Pages.Navbar;
using AppointmentPlannerTests.Structs;
using LoggerLibrary.Interfaces.Loggers;
using TechTalk.SpecFlow;
using WebDriverLibrary.Interfaces.WebDrivers;

namespace AppointmentPlannerTests.Steps.Navbar;

[Binding]
public sealed class NavbarStep
{
	private readonly ScenarioContext _scenarioContext;
	private readonly NavbarPage _navbarPage;

	public NavbarStep(ScenarioContext scenarioContext)
	{
		_scenarioContext = scenarioContext;

		var webDriverService = _scenarioContext[ScenarioContextKey.WebDriverService] as IWebDriverService;
		var loggerService = _scenarioContext[ScenarioContextKey.LoggerService] as ILoggerService;

		_navbarPage = new NavbarPage(webDriverService!, loggerService!);
	}

	[Given(@"I navigate to the Doctors page")]
	public void GivenINavigateToTheDoctorsPage()
	{
		_navbarPage.NavigateToDoctorsPage();
	}
}
