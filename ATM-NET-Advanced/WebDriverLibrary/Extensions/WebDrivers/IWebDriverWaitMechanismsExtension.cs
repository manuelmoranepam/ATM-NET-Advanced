using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace WebDriverLibrary.Extensions.WebDrivers;

public static class IWebDriverWaitMechanismsExtension
{
	public static void WaitUntilElementIsVisible(this IWebDriver webDriver, By locator, TimeSpan timeout, TimeSpan pollingInterval)
	{
		NullCheckAllParameters(webDriver, locator, timeout, pollingInterval);

		var wait = new WebDriverWait(new SystemClock(), webDriver, timeout, pollingInterval);

		wait.Until(ExpectedConditions.ElementIsVisible(locator));
	}

	public static void WaitUntilElementExists(this IWebDriver webDriver, By locator, TimeSpan timeout, TimeSpan pollingInterval)
	{
		NullCheckAllParameters(webDriver, locator, timeout, pollingInterval);

		var wait = new WebDriverWait(new SystemClock(), webDriver, timeout, pollingInterval);

		wait.Until(ExpectedConditions.ElementExists(locator));
	}

	public static void WaitUntilElementIsClickable(this IWebDriver webDriver, By locator, TimeSpan timeout, TimeSpan pollingInterval)
	{
		NullCheckAllParameters(webDriver, locator, timeout, pollingInterval);

		var wait = new WebDriverWait(new SystemClock(), webDriver, timeout, pollingInterval);

		wait.Until(ExpectedConditions.ElementToBeClickable(locator));
	}

	public static void WaitForCondition(this IWebDriver webDriver, Func<IWebDriver, bool> condition, TimeSpan timeout, TimeSpan pollingInterval)
	{
		NullCheckAllParameters(webDriver, condition, timeout, pollingInterval);

		var wait = new WebDriverWait(new SystemClock(), webDriver, timeout, pollingInterval);

		wait.Until(condition);
	}

	private static void NullCheckAllParameters(IWebDriver webDriver, By locator, TimeSpan timeout, TimeSpan pollingInterval)
	{
		ArgumentNullException.ThrowIfNull(webDriver);
		ArgumentNullException.ThrowIfNull(locator);
		ArgumentOutOfRangeException.ThrowIfLessThan(timeout, TimeSpan.FromSeconds(1));
		ArgumentOutOfRangeException.ThrowIfLessThan(pollingInterval, TimeSpan.FromMilliseconds(1));
		ArgumentOutOfRangeException.ThrowIfLessThan(timeout, pollingInterval);
	}

	private static void NullCheckAllParameters(IWebDriver webDriver, Func<IWebDriver, bool> condition, TimeSpan timeout, TimeSpan pollingInterval)
	{
		ArgumentNullException.ThrowIfNull(webDriver);
		ArgumentNullException.ThrowIfNull(condition);
		ArgumentOutOfRangeException.ThrowIfLessThan(timeout, TimeSpan.FromSeconds(1));
		ArgumentOutOfRangeException.ThrowIfLessThan(pollingInterval, TimeSpan.FromMilliseconds(1));
		ArgumentOutOfRangeException.ThrowIfLessThan(timeout, pollingInterval);
	}
}
