using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using WebDriverLibrary.Enums;
using WebDriverLibrary.Interfaces.Configurations;
using WebDriverLibrary.Interfaces.WebDrivers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace WebDriverLibrary.WebDrivers;

public class SeleniumWebDriverService : IWebDriverService
{
	private readonly IWebDriverConfiguration _webDriverConfiguration;
	private readonly IWebDriver _webDriver;

	public SeleniumWebDriverService(IWebDriverConfiguration webDriverConfiguration)
	{
		ArgumentNullException.ThrowIfNull(webDriverConfiguration);

		_webDriverConfiguration = webDriverConfiguration;

		_webDriver = CreateWebDriver();

		ApplyConfigurations();
	}

	private IWebDriver CreateWebDriver()
	{
		return _webDriverConfiguration.BrowserType switch
		{
			BrowserType.Chrome => CreateChromeDriver(),
			BrowserType.Edge => CreateEdgeDriver(),
			BrowserType.Firefox => CreateFirefoxDriver(),
			_ => throw new NotSupportedException($"Browser type '{_webDriverConfiguration.BrowserType}' is not supported.")
		};
	}

	private IWebDriver CreateChromeDriver()
	{
		_ = new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

		var options = GetChromeDriverOptions();

		var driver = new ChromeDriver(options);

		return driver;
	}

	private ChromeOptions GetChromeDriverOptions()
	{
		var options = new ChromeOptions();

		if (_webDriverConfiguration.IsIncognito)
		{
			options.AddArgument("--incognito");
		}

		ApplyGeneralOptions(options);

		return options;
	}

	private void ApplyGeneralOptions(dynamic options)
	{
		if (_webDriverConfiguration.IsHeadless)
		{
			options.AddArgument("--headless");
		}

		options.PageLoadStrategy = _webDriverConfiguration.PageLoadStrategy;
	}

	private IWebDriver CreateEdgeDriver()
	{
		_ = new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);

		var options = GetEdgeOptions();

		var driver = new EdgeDriver(options);

		return driver;
	}

	private EdgeOptions GetEdgeOptions()
	{
		var options = new EdgeOptions();

		if (_webDriverConfiguration.IsIncognito)
		{
			options.AddArgument("--inprivate");
		}

		ApplyGeneralOptions(options);

		return options;
	}

	private IWebDriver CreateFirefoxDriver()
	{
		_ = new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);

		var options = GetFirefoxOptions();

		var driver = new FirefoxDriver(options);

		return driver;
	}

	private FirefoxOptions GetFirefoxOptions()
	{
		var options = new FirefoxOptions();

		if (_webDriverConfiguration.IsIncognito)
		{
			options.AddArgument("--private");
		}

		ApplyGeneralOptions(options);

		return options;
	}

	private void ApplyConfigurations()
	{
		_webDriver.Manage().Timeouts().PageLoad = _webDriverConfiguration.PageLoadTimeout;
		_webDriver.Manage().Timeouts().ImplicitWait = _webDriverConfiguration.ImplicitTimeout;
		_webDriver.Manage().Timeouts().AsynchronousJavaScript = _webDriverConfiguration.AsyncJavaScriptTimeout;
		_webDriver.Manage().Cookies.DeleteAllCookies();

		if (_webDriverConfiguration.IsMaximized)
		{
			_webDriver.Manage().Window.Maximize();
		}
	}

	public IWebDriver GetWebDriver()
	{
		return _webDriver;
	}

	public IWebDriverConfiguration GetWebDriverConfiguration()
	{
		return _webDriverConfiguration;
	}

	public void DisposeWebDriver()
	{
		if (_webDriver is not null)
		{
			_webDriver.Close();
			_webDriver.Quit();
			_webDriver.Dispose();
		}
	}

	public void NavigateTo(string url)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(url);

		_webDriver.Navigate().GoToUrl(url);
	}

	public string GetCurrentUrl()
	{
		return _webDriver.Url;
	}

	public string GetCurrentPageTitle()
	{
		return _webDriver.Title;
	}
}
