﻿using Core;
using NUnit.Framework;
using UI.Pages;
using UI.Steps;
using UI.enums;

namespace UI.Tests
{
	[TestFixture]
	public class Test : BaseTest
	{
        private static MainPage mainPage = new MainPage();

        [Test]
        public void LoginAndLogout()
        {
			CustomLogger.LogInfo(LogLevel.Info, "Go to Login page");
			mainPage.ToLoginPage();
			CustomLogger.LogInfo(LogLevel.Info, "Login to account");
			string actualUsername = LoginPagesSteps.Login()
			.GetLoggedUsername();
			CustomLogger.LogInfo(LogLevel.Info, "Verify username is right");
			Assert.That(actualUsername, Is.EqualTo(TestDataReader.GetTestUsername()));

            CustomLogger.LogInfo(LogLevel.Info, "Logout from account");
            var hasReturnedToMainPage = MainPageSteps.Logout()
                .ToMainPage()
                .IsPageVisible();

            CustomLogger.LogInfo(LogLevel.Info, "Verify Main Page is visible");
            Assert.That(hasReturnedToMainPage, Is.True);
		}

        [Test]
        public void FindArticle()
        {
            CustomLogger.LogInfo(LogLevel.Info, "Go to Main Page");
            string expectedTitle = "Mikhail Lomonosov";
            CustomLogger.LogInfo(LogLevel.Info, $"Start search {expectedTitle}");
            string actualTitle = MainPageSteps.Search(expectedTitle)
            .GetTitle();
            CustomLogger.LogInfo(LogLevel.Info, "Verify that loaded right article page");
            Assert.That(expectedTitle, Is.EqualTo(actualTitle));
        }

        [Test]
        public void CheckArticleEditHistory()
        {
            CustomLogger.LogInfo(LogLevel.Info, "Go to Login page");
            mainPage.ToLoginPage();
            CustomLogger.LogInfo(LogLevel.Info, "Login to account");
            string actualUsername = LoginPagesSteps.Login()
            .GetLoggedUsername();
            CustomLogger.LogInfo(LogLevel.Info, "Verify username is right");
            Assert.That(actualUsername, Is.EqualTo(TestDataReader.GetTestUsername()));

            CustomLogger.LogInfo(LogLevel.Info, "Click to random article");
            bool IsEditPageVisible = mainPage.ClickToRandomArticle()
            .ClickToViewHistory()
            .IsPageVisible();
            CustomLogger.LogInfo(LogLevel.Info, "Verify Edit page is visible");
            Assert.That(IsEditPageVisible, Is.True);
        }

        [Test]
        public void CheckNotLoggedWarningOnEditPage()
        {
            CustomLogger.LogInfo(LogLevel.Info, "Go to Main page");
            mainPage.ClickToSideMenu();
            bool IsNotLoggedWarningDisplayed = mainPage.ClickToRandomArticle()
            .ClickToEdit()
            .IsNotLoggedWarningDisplayed();
            CustomLogger.LogInfo(LogLevel.Info, "Verify warning for not logged user displayed");
            Assert.That(IsNotLoggedWarningDisplayed, Is.True);
        }

        [Test]
        public void CheckSwitchLanguages()
        {
            var languagesPage = new LanguagesPage();
            Driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/List_of_Wikipedias");
            string expectedTitle = "Vikipediya";
            CustomLogger.LogInfo(LogLevel.Info, "Go to Languages Page");
            languagesPage.ClickPopUpWindowButton();
            languagesPage.ClickRussianLanguageButton();
            Assert.That(Driver.Title, Is.EqualTo("Языковые разделы Википедии — Википедия"));
            languagesPage.ClickEnglishLanguageButton();
            Assert.That(Driver.Title, Is.EqualTo("List of Wikipedias - Wikipedia"));
            languagesPage.ClickUzbekLanguageButton();
            languagesPage.ClickUzbekWikipediaButton();
            string actualTitle = Driver.Title;
            Assert.That(expectedTitle, Is.EqualTo(actualTitle));
        }
    }
}
