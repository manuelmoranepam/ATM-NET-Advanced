using System;
using WebDriverLibrary.Extensions.WebDrivers;

namespace AppointmentPlannerTests.Pages.Navbar;

public partial class NavbarPage
{
	private void ClickDoctorsMenuItem()
	{
		try
		{
			_webDriver.WaitUntilElementIsClickable(_doctorsMenuItemLocator,
				_webDriverService.GetWebDriverConfiguration().LongTimeout,
				_webDriverService.GetWebDriverConfiguration().PollingIntervalTimeout);

			DoctorsMenuItem.Click();
		}
		catch (Exception e)
		{
			_loggerService.LogError(e, _doctorsMenuItemClickFailedMessage, _doctorsMenuItemLocator);

			throw;
		}
	}
}
