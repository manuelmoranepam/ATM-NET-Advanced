using OpenQA.Selenium;
using WebDriverLibrary.Interfaces.Configurations;

namespace WebDriverLibrary.Interfaces.WebDrivers;

public interface IWebDriverService
{
	void DisposeWebDriver();
	string GetCurrentPageTitle();
	string GetCurrentUrl();
	IWebDriver GetWebDriver();
	IWebDriverConfiguration GetWebDriverConfiguration();
	void NavigateTo(string url);
}
