﻿using NUnit.Framework;
using UI.WebDriver;
using OpenQA.Selenium;
using UI.Utils;
using NUnit.Framework.Interfaces;
using UI.Pages;

namespace UI.Tests
{
	public abstract class BaseTest
	{
		protected static IWebDriver Driver;
		protected static ICustomLogger CustomLogger;

		protected static MainPage MainPage;
		protected static LoginPage LoginPage;
		protected static AboutPage AboutPage;

		[SetUp]
		public void Setup()
		{
			Driver = Browser.GetDriver();
			CustomLogger = new CustomLogger(Driver);
			CustomLogger.LogInfo(Utils.LogLevel.Info, $"Start Test [{TestContext.CurrentContext.Test.Name}]");
			Browser.WindowMaximize();
			Browser.StartNavigate();

			// Initializing pages
			MainPage = new MainPage();
			LoginPage = new LoginPage();
			AboutPage = new AboutPage();
		}

		[TearDown]
		public void Quit()
		{
			TestStatus NUnit_status = TestContext.CurrentContext.Result.Outcome.Status;

			if (NUnit_status.Equals(TestStatus.Failed))
			{
				var failMessage = $"[{TestContext.CurrentContext.Test.Name}] Test failed with Status: " +
					TestContext.CurrentContext.Result.Message;
				CustomLogger.LogInfo(Utils.LogLevel.Error, failMessage);
			}
			else
			{
				var statusMessage = $"[{TestContext.CurrentContext.Test.Name}] Test ended with Status: " +
					TestContext.CurrentContext.Result.Outcome.Status.ToString();
				CustomLogger.LogInfo(Utils.LogLevel.Info, statusMessage);

			}
			Browser.QuitBrowser();
		}
	}
}
