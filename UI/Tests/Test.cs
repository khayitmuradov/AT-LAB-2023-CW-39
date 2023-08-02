﻿using Core;
using NUnit.Framework;
using UI.Pages;
using UI.Steps;

namespace UI.Tests
{
	[TestFixture]
	public class Test : BaseTest
	{
        private static MainPage mainPage = new MainPage();

        [Test]
        public void LoginAndLogout()
        {
			CustomLogger.LogInfo(Utils.LogLevel.Info, "Go to Login page");
			mainPage.ToLoginPage();
			CustomLogger.LogInfo(Utils.LogLevel.Info, "Login to account");
			string actualUsername = LoginPagesSteps.Login()
			.GetLoggedUsername();
			CustomLogger.LogInfo(Utils.LogLevel.Info, "Verify username is right");
			Assert.That(actualUsername, Is.EqualTo(TestDataReader.GetTestUsername()));

            CustomLogger.LogInfo(Utils.LogLevel.Info, "Logout from account");
            var hasReturnedToMainPage = MainPageSteps.Logout()
                .ToMainPage()
                .IsPageVisible();

            CustomLogger.LogInfo(Utils.LogLevel.Info, "Verify Main Page is visible");
            Assert.That(hasReturnedToMainPage, Is.True);
		}

        [Test]
        public void FindArticle()
        {
            CustomLogger.LogInfo(Utils.LogLevel.Info, "Go to Main Page");
            string expectedTitle = "Mikhail Lomonosov";
            CustomLogger.LogInfo(Utils.LogLevel.Info, $"Start search {expectedTitle}");
            string actualTitle = MainPageSteps.Search(expectedTitle)
            .GetTitle();
            CustomLogger.LogInfo(Utils.LogLevel.Info, "Verify that loaded right article page");
            Assert.That(expectedTitle, Is.EqualTo(actualTitle));
        }

        [Test]
        public void CheckArticleEditHistory()
        {
            CustomLogger.LogInfo(Utils.LogLevel.Info, "Go to Login page");
            mainPage.ToLoginPage();
            CustomLogger.LogInfo(Utils.LogLevel.Info, "Login to account");
            string actualUsername = LoginPagesSteps.Login()
            .GetLoggedUsername();
            CustomLogger.LogInfo(Utils.LogLevel.Info, "Verify username is right");
            Assert.That(actualUsername, Is.EqualTo(TestDataReader.GetTestUsername()));

            CustomLogger.LogInfo(Utils.LogLevel.Info, "Click to random article");
            bool IsEditPageVisible = mainPage.ClickToRandomArticle()
            .ClickToViewHistory()
            .IsPageVisible();
            CustomLogger.LogInfo(Utils.LogLevel.Info, "Verify Edit page is visible");
            Assert.That(IsEditPageVisible, Is.True);
        }

        [Test]
        public void CheckNotLoggedWarningOnEditPage()
        {
            CustomLogger.LogInfo(Utils.LogLevel.Info, "Go to Main page");
            mainPage.ClickToSideMenu();
            bool IsNotLoggedWarningDisplayed = mainPage.ClickToRandomArticle()
            .ClickToEdit()
            .IsNotLoggedWarningDisplayed();
            CustomLogger.LogInfo(Utils.LogLevel.Info, "Verify warning for not logged user displayed");
            Assert.That(IsNotLoggedWarningDisplayed, Is.True);
        }
    }
}
