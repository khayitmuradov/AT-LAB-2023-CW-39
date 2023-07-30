﻿using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using static UI.WebDriver.WebDriverFactory;
using System;

namespace UI.WebDriver
{
	public class Browser
	{
		public static BrowserType _currentBrowser;
		private static Browser _currentInstance;
		private static string _browser;
		private static int ImplWait;
		private static IWebDriver webDriver;
		private static Actions _actions;
		private static IJavaScriptExecutor _jsExecuter;
		private static IWebDriver driver => GetDriver();

		private static void InitParams()
		{
			ImplWait = Convert.ToInt32(Configuration.ElementTimeout);
			_browser = Configuration.Browser;
			Enum.TryParse(_browser, out _currentBrowser);
		}

		private Browser()
		{
			InitParams();
			webDriver = WebDriverFactory.GetDriver(_currentBrowser);
		}

		public static IWebDriver GetDriver()
		{
			if (webDriver == null)
			{
				_currentInstance = new Browser();
			}

			return webDriver;
		}

		public static void WindowMaximaze()
		{
			driver.Manage().Window.Maximize();
		}

		public static void NavigateTo(string url)
		{
			driver.Navigate().GoToUrl(url);
		}

		public static void StartNavigate()
		{
			driver.Navigate().GoToUrl(Configuration.StartUrl);
		}

		public static void QuiteBrowser()
		{
			driver.Quit();
			_currentInstance = null;
			webDriver = null;
			_browser = null;
		}

		public static Actions GetActions()
		{
			_actions = new Actions(GetDriver());
			return _actions;
		}

		public static IJavaScriptExecutor GetJSExecuter()
		{
			_jsExecuter = (IJavaScriptExecutor)GetDriver();
			return _jsExecuter;
		}
	}
}
