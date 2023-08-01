﻿using OpenQA.Selenium;
using UI.Utils;

namespace UI.Pages
{
    public class EditPage : AbstractPage
    {
        private static readonly By notLoggedWarning = By.CssSelector(".mw-halign-left");

        public bool IsNotLoggedWarningDisplayed(int timeSeconds)
        {            
            return WebDriverExtension.IsElementVisible(notLoggedWarning, timeSeconds);
        }
    }
}